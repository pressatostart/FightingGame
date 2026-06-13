using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class CanBounceGroundCondition : FrameDataCondition
    {
        [SerializeField] private bool Not;

        public override bool Check(ref SakugaActor Actor)
        {
            if (!Actor.CanBounce()) return false;
            if (Not) return !Actor.IsBouncingY();

            return Actor.IsBouncingY();
        }
    }
}
