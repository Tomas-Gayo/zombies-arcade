using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "KeySORequest")]
	public sealed class KeySORequestGameEventListener : BaseGameEventListener<KeySORequest, KeySORequestGameEvent, KeySORequestUnityEvent>
	{
	}
}