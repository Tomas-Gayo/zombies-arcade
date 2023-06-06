using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [Header("Dependencies")]
    public TextMeshProUGUI shieldTxt;
    public TextMeshProUGUI healthTxt;
    public Transform shieldBar;
    public Transform healthBar;

    // Private references
    private Vector3 shieldScale;


    public void SetupHUD(HealthSO health)
    {
        // Shield Bar
        shieldBar.transform.localScale = new Vector3(health.CurrentShield * 0.01f, 1, 1);
        shieldTxt.text = health.CurrentShield.ToString() + " %";

        // Health Bar
        healthBar.transform.localScale = new Vector3(health.CurrentHealth * 0.01f, 1, 1);
        healthTxt.text = health.CurrentHealth.ToString() + " %";
    }
}
