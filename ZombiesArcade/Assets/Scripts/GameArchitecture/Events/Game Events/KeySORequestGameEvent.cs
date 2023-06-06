using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "KeySORequestGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "Key Request",
	    order = 120)]
	public sealed class KeySORequestGameEvent : GameEventBase<KeySORequest>
	{
	}
}