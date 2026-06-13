using UnityEngine;

namespace SakugaEngine.Resources
{	
	[CreateAssetMenu(fileName = "New Hit Notifications List", menuName = "Sakuga Engine/Hit Notifications List", order = 0)]
	public class HitNotificationList : ScriptableObject
	{
		public HitNotificationElement[] Elements;
	}
}