using UnityEngine;
using System;
using SakugaEngine.Global;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class AtRandomFrameCondition : FrameDataCondition
    {
        [SerializeField] private Vector2Int Range;
        [SerializeField] private bool Not;

        public override bool Check(ref SakugaActor Actor)
        {
            if (Actor.StateManager == null) return false;

            int random = RNG.Next(Range.x, Range.y);

            if (Not) return Actor.StateManager.CurrentStateFrame != random;
            return Actor.StateManager.CurrentStateFrame == random;
        }
    }
}
