using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class RestoreHealthSORequestReference : BaseReference<RestoreHealthSORequest, RestoreHealthSORequestVariable>
	{
	    public RestoreHealthSORequestReference() : base() { }
	    public RestoreHealthSORequestReference(RestoreHealthSORequest value) : base(value) { }
	}
}