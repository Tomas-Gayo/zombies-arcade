using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Scriptable Objects/Weapon", order = 1)]
public class WeaponSO : ScriptableObject
{
    [Header("Dependencies")]
    [SerializeField] private GameObject weaponPrefab;

    [Header("Configuration")]
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField, Range(0f,0.1f)] private float spread;
    [SerializeField] private float timeBetweenShooting;
    [SerializeField] private float reloadTime;
    [SerializeField] private int bulletsPerShot;
    [SerializeField] private int magazineSize;
    [SerializeField] private int baseAmmo;
    [SerializeField] private int totalAmmo;

    [Header("Audios")]
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public AudioClip emptyMagazineSound;

    [Header("UI")]
    [SerializeField] private Sprite icon;

    // Private variables
    private int currentAmmo;
    private bool canShoot;
    private bool isReloading;

    public float Damage => damage;
    public float Range => range;
    public float Spread => spread;
    public float TimeBetweenShooting => timeBetweenShooting;
    public float ReloadTime => reloadTime;
    public int BulletsPerShot => bulletsPerShot;
    public int TotalAmmo
    {
        get => totalAmmo;
        set => totalAmmo += value;
    }
    public int CurrentAmmo
    {
        get { return currentAmmo; }
        set 
        { 
            currentAmmo = value;
            if (currentAmmo < 0)
                currentAmmo = 0;
            else if (currentAmmo > magazineSize)
                currentAmmo = magazineSize;
        }
    }

    public Sprite Icon => icon;

    public bool CanShoot
    {
        get { return canShoot; }
        set { canShoot = value; }
    }

    public bool IsReloading
    {
        get { return isReloading; }
        set { isReloading = value; }
    }

    private void OnEnable()
    {
        canShoot = true;
        InitializeAmmo();
    }

    private void InitializeAmmo()
    {
        totalAmmo = baseAmmo;
        if (totalAmmo > magazineSize)
            currentAmmo = magazineSize;
        else if (totalAmmo < magazineSize)
            currentAmmo = totalAmmo;
    }

    public void Reload()
    {
        if (totalAmmo > magazineSize)
        {
            currentAmmo = magazineSize;
            totalAmmo -= magazineSize;
        }
        else if (totalAmmo < magazineSize)
        {
            currentAmmo = totalAmmo;
            totalAmmo = 0;
        }
    }
}

