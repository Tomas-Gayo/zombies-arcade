using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "WeaponSORequest")]
	public sealed class WeaponSORequestGameEventListener : BaseGameEventListener<WeaponSORequest, WeaponSORequestGameEvent, WeaponSORequestUnityEvent>
	{
	}
}