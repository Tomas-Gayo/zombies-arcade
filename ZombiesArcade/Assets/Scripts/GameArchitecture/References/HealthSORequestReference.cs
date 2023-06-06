using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class HealthSORequestReference : BaseReference<HealthSORequest, HealthSORequestVariable>
	{
	    public HealthSORequestReference() : base() { }
	    public HealthSORequestReference(HealthSORequest value) : base(value) { }
	}
}