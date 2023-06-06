using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "RestoreHealthSORequest")]
	public sealed class RestoreHealthSORequestGameEventListener : BaseGameEventListener<RestoreHealthSORequest, RestoreHealthSORequestGameEvent, RestoreHealthSORequestUnityEvent>
	{
	}
}