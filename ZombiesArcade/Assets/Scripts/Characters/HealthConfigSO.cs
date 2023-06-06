using UnityEngine;

[CreateAssetMenu(fileName = "HealthConfig", menuName = "Scriptable Objects/Health Config")]
public class HealthConfigSO : ScriptableObject
{
    [SerializeField] private float initialShield;
    [SerializeField] private float initialHealth;

    public float InitialShield => initialShield;
    public float InitialHealth => initialHealth;
}
