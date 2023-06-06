using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "HealthSORequestGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "HealthRequest",
	    order = 120)]
	public sealed class HealthSORequestGameEvent : GameEventBase<HealthSORequest>
	{
	}
}