using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class MatchAnimation
    {
        public string Name;
        public int Duration;
        public int DelaySubtract;
        public int AnnouncerLineIndex = -1;
    }
}
