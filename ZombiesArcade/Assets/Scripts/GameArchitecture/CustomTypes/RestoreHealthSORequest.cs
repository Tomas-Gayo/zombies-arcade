[System.Serializable]
public class RestoreHealthSORequest
{
    public float healthAmount;
    public float shieldAmount;

    public RestoreHealthSORequest(float healthAmount, float shieldAmount)
    {
        this.healthAmount = healthAmount;
        this.shieldAmount = shieldAmount;
    }
}
