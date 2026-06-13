using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class CanBounceCondition : FrameDataCondition
    {
        [SerializeField] private bool GroundBounce;
        [SerializeField] private bool WallBounce;
        [SerializeField] private bool Not;

        public override bool Check(ref SakugaActor Actor)
        {
            bool canGroundBounce = GroundBounce && (Not ? Actor.IsBouncingY() : !Actor.IsBouncingY());
            bool canWallBounce = WallBounce && (Not ? Actor.IsBouncingX() : !Actor.IsBouncingX());

            return canGroundBounce || canWallBounce;
        }
    }
}
