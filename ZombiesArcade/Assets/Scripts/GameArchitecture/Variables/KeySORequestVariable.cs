using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class KeySORequestEvent : UnityEvent<KeySORequest> { }

	[CreateAssetMenu(
	    fileName = "KeySORequestVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Key Request",
	    order = 120)]
	public class KeySORequestVariable : BaseVariable<KeySORequest, KeySORequestEvent>
	{
	}
}