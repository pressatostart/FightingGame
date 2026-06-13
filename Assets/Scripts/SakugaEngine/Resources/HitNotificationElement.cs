using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
	[Serializable]
    public class HitNotificationElement
    {
        public string NotificationName;
        [TextArea] public string Description;
        public int AnnouncerVoiceLine = -1;
    }
}
