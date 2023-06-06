using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Dependencies")]
    public HealthUI healthUI;

    private HealthSO health;

    public void SetupHUD(HealthSORequest request)
    {
        // Save reference
        health = request.healthSO;

        healthUI.SetupHUD(health);
    }
}
