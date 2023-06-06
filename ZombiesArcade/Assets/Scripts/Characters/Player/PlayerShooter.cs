using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using ScriptableObjectArchitecture;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PlayerInputs))]
public class PlayerShooter : MonoBehaviour
{
    [Header("Configuration")]
    public Transform firePoint;

    [Header("Dependencies")]
    [SerializeField] private WeaponSO weapon;

    [Header("Effects")]
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    [Header("Broadcasting on")]
    public WeaponSORequestGameEvent updateWeaponUI;
    public UnityEvent OnReadyToShot;
    public UnityEvent OnReload;

    // Private Dependencies
    private AudioSource audioSource;
    private PlayerInputs input;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        input = GetComponent<PlayerInputs>();

        WeaponUITrigger();
    }

    private void Update()
    {
        // Player Inputs
        if (input.shoot && weapon.CanShoot && weapon.CurrentAmmo > 0)
            Shoot();
        else if (input.realod && !weapon.IsReloading && weapon.CurrentAmmo <= 0)
            StartCoroutine(Reload());
    }

    private void Shoot()
    {
        weapon.CanShoot = false;

        ShotEffect();

        for (int i = 0; i < weapon.BulletsPerShot; i++)
        {

            float x = Random.Range(-weapon.Spread, weapon.Spread);
            float y = Random.Range(-weapon.Spread, weapon.Spread);
            Ray rayOrigin = Camera.main.ViewportPointToRay(new(0.5f + x, 0.5f + y, 0.0f));

            if (Physics.Raycast(rayOrigin, out RaycastHit hit, weapon.Range))
            {
                if (hit.collider.TryGetComponent(out Damageable damageable))
                {
                    damageable.ReceiveAnAttack(weapon.Damage);
                }

                // Añadir pool ?
                GameObject imapact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(imapact, 2.0f);
            } 
        }
        weapon.CurrentAmmo--;
        WeaponUITrigger();
        Invoke(nameof(ResetShot), weapon.TimeBetweenShooting);
    }

    private IEnumerator Reload()
    {
        weapon.IsReloading = true;
        OnReload?.Invoke();
        audioSource.clip = weapon.reloadSound;
        audioSource.Play();

        yield return new WaitForSeconds(weapon.ReloadTime);

        // Reload and update UI
        weapon.Reload();
        WeaponUITrigger();
        OnReadyToShot?.Invoke();
        weapon.IsReloading = false;
    }

    private void WeaponUITrigger()
    {
        var weaponRequest = new WeaponSORequest(
            weapon: weapon
        );
        updateWeaponUI?.Raise(weaponRequest);
    }

    private void ResetShot()
    {
        weapon.CanShoot = true;
    }

    private void ShotEffect()
    {
        audioSource.clip = weapon.fireSound;
        audioSource.Play();
        muzzleFlash.Play();
    }

    public void RestoreAmmo(int value)
    {
        weapon.TotalAmmo = value;
        WeaponUITrigger();
    }
}

