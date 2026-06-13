using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class CheckTimerCondition : FrameDataCondition
    {
        [SerializeField] private int ByIndex = -1;
        [SerializeField] private string ByName;
        [SerializeField] private bool Not;

        public override bool Check(ref SakugaActor Actor)
        {
            if (Actor.Parameters == null) return false;
            FrameTimer timer = null;
            if (ByIndex >= 0) Actor.Parameters.GetTimer(ByIndex);
            else if (ByName != "") timer = Actor.Parameters.GetTimer(ByName);
            if (timer == null) return false;

            if (Not) return !timer.IsRunning();
            return timer.IsRunning();
        }
    }
}
