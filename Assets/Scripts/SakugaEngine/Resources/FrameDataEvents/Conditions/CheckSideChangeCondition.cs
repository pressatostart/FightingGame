using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class CheckSideChangeCondition : FrameDataCondition
    {
        [SerializeField] private bool Not;

        public override bool Check(ref SakugaActor Actor)
        {
            if (Actor.Body == null) return false;

            if (Not) return (Actor.Body.IsLeftSide && Actor.InputSide() > 0) || (!Actor.Body.IsLeftSide && Actor.InputSide() < 0);
            return (Actor.Body.IsLeftSide && Actor.InputSide() < 0) || (!Actor.Body.IsLeftSide && Actor.InputSide() > 0);
        }
    }
}
