using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class StateEndedCondition : FrameDataCondition
    {
        [SerializeField] private bool Not;
        public override bool Check(ref SakugaActor Actor)
        {
            if (Actor.StateManager == null) return false;

            if (Not) return Actor.StateManager.CurrentStateFrame < Actor.StateManager.GetCurrentState().AnimationData.Duration;
            return Actor.StateManager.CurrentStateFrame >= Actor.StateManager.GetCurrentState().AnimationData.Duration;
        }
    }
}
