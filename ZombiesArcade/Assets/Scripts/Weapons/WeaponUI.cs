using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TextMeshProUGUI currentAmmoTxt;
    [SerializeField] private TextMeshProUGUI totalAmmoTxt;
    [SerializeField] private Image weaponIcon;

    public void SetupHUD(WeaponSO weapon)
    {
        currentAmmoTxt.text = weapon.CurrentAmmo.ToString();
        totalAmmoTxt.text = weapon.TotalAmmo.ToString();
        weaponIcon.sprite = weapon.Icon;
    }
}
