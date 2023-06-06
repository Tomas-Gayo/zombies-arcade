using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class WeaponSORequestReference : BaseReference<WeaponSORequest, WeaponSORequestVariable>
	{
	    public WeaponSORequestReference() : base() { }
	    public WeaponSORequestReference(WeaponSORequest value) : base(value) { }
	}
}