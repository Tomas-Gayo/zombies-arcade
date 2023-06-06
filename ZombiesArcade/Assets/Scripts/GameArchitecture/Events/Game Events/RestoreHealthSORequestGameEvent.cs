using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "RestoreHealthSORequestGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "Restore Health",
	    order = 120)]
	public sealed class RestoreHealthSORequestGameEvent : GameEventBase<RestoreHealthSORequest>
	{
	}
}