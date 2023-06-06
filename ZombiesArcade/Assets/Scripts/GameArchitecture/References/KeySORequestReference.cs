using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class KeySORequestReference : BaseReference<KeySORequest, KeySORequestVariable>
	{
	    public KeySORequestReference() : base() { }
	    public KeySORequestReference(KeySORequest value) : base(value) { }
	}
}