using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "HealthSORequest")]
	public sealed class HealthSORequestGameEventListener : BaseGameEventListener<HealthSORequest, HealthSORequestGameEvent, HealthSORequestUnityEvent>
	{
	}
}