using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class WeaponSORequestEvent : UnityEvent<WeaponSORequest> { }

	[CreateAssetMenu(
	    fileName = "WeaponSORequestVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "WeaponRequest",
	    order = 120)]
	public class WeaponSORequestVariable : BaseVariable<WeaponSORequest, WeaponSORequestEvent>
	{
	}
}