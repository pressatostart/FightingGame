using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class AtFrameRangeCondition : FrameDataCondition
    {
        [SerializeField] private int MinFrame;
        [SerializeField] private int MaxFrame;
        [SerializeField] private bool Not;

        public override bool Check(ref SakugaActor Actor)
        {
            if (Actor.StateManager == null) return false;

            if (Not) return Actor.StateManager.CurrentStateFrame < MinFrame || Actor.StateManager.CurrentStateFrame > MaxFrame;
            return Actor.StateManager.CurrentStateFrame >= MinFrame && Actor.StateManager.CurrentStateFrame <= MaxFrame;
        }
    }
}
