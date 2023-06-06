using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class HealthSORequestEvent : UnityEvent<HealthSORequest> { }

	[CreateAssetMenu(
	    fileName = "HealthSORequestVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "HealthRequest",
	    order = 120)]
	public class HealthSORequestVariable : BaseVariable<HealthSORequest, HealthSORequestEvent>
	{
	}
}