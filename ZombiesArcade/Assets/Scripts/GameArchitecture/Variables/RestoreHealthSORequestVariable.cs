using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class RestoreHealthSORequestEvent : UnityEvent<RestoreHealthSORequest> { }

	[CreateAssetMenu(
	    fileName = "RestoreHealthSORequestVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Restore Health",
	    order = 120)]
	public class RestoreHealthSORequestVariable : BaseVariable<RestoreHealthSORequest, RestoreHealthSORequestEvent>
	{
	}
}