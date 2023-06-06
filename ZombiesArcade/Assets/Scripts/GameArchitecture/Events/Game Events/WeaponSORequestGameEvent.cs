using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "WeaponSORequestGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "WeaponRequest",
	    order = 120)]
	public sealed class WeaponSORequestGameEvent : GameEventBase<WeaponSORequest>
	{
	}
}