using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Dependencies")]
    public WeaponUI weaponUI;

    // Private references
    private WeaponSO weapon;

    public void SetupWeaponHUD(WeaponSORequest request)
    {
        // Save reference for later
        weapon = request.weapon;

        weaponUI.SetupHUD(weapon);
    }
}
