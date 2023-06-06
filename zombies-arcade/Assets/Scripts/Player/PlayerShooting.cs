using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    [Header("Aiming parameters")]
    [Tooltip("Turn speed of the player towards the aiming position.")]
    public float turnSpeed = 15;
    [Tooltip("The time it takes the animation to aim.")]
    public float aimDuration = 0.3f;
    [Tooltip("Field of view when aiming.")]
    public float aimFOV = 20f;
    [Tooltip("Reference to the aiming rig.")]
    public Rig aimLayer;
    [Header("Shooting parameters.")]
    [Tooltip("Total ammo of the weapon.")]
    public int totalAmmo = 100;
    [Tooltip("Percentage amount of ammo lose when shot.")]
    public float ammoSubs = 5f;
    [Tooltip("UI slider to display the total of ammunition.")]
    public Image ammoBar;
    [Tooltip("UI text to display the percentage of ammo.")]
    public TextMeshProUGUI ammoText;

    [Tooltip("Feedback meesage for the player.")]
    public TextMeshProUGUI feedbackText;

    private float defaultFOV;       // Field of view by default
    private float currentAmmo;        // Current value of the ammo
    private Shot weapon;            // Reference to the Shot script

    private bool readyToShot;       // To know if the player is ready to shot

    private void Awake()
    {
        weapon = GetComponentInChildren<Shot>();
    }

    private void Start()
    {
        // Set the field of view 
        defaultFOV = Camera.main.fieldOfView;

        currentAmmo = totalAmmo;
    }

    private void Update()
    {
        // Rotates the player towards the aiming position
        float yCam = Camera.main.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yCam, 0), turnSpeed * Time.deltaTime);

        // Shoot
        if (aimLayer)
        {
            if (Input.GetButton("Fire2"))
            {
                readyToShot = true;
                aimLayer.weight += Time.deltaTime / aimDuration;
                Camera.main.fieldOfView = aimFOV;
            }
            else
            {
                readyToShot = false;
                aimLayer.weight -= Time.deltaTime / aimDuration;
                Camera.main.fieldOfView = defaultFOV;
            } 
        }

        if (Input.GetButtonDown("Fire1") && readyToShot)
        {

            currentAmmo -= totalAmmo * (ammoSubs / 100);
            if(currentAmmo < 0)
            {
                currentAmmo = 0;
                // Display feedback message
            }
            else
                weapon.Shooting();
        }

        ammoBar.fillAmount = currentAmmo / totalAmmo;
        ammoText.text = ((currentAmmo / totalAmmo) * 100).ToString() + " %";
    }

    public void RestoreAmmo(int ammo)
    {
        currentAmmo += ammo;
        if (currentAmmo > totalAmmo)
            currentAmmo = totalAmmo;
    }
}
