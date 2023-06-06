using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "LoadSceneRequestGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "Scene Request",
	    order = 120)]
	public sealed class LoadSceneRequestGameEvent : GameEventBase<LoadSceneRequest>
	{
	}
}