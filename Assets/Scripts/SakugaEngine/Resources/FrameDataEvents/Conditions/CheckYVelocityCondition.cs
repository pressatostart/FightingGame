using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public partial class CheckYVelocityCondition : FrameDataCondition
    {
        [SerializeField] private bool Absolute;
        [SerializeField] private int Value;
        [SerializeField] private Global.CompareMode CompareMode;

        public override bool Check(ref SakugaActor Actor)
        {
            if (Actor.Body == null) return false;

            int vel = Absolute ? Mathf.Abs(Actor.Body.FixedVelocity.y) : Actor.Body.FixedVelocity.y;

            switch (CompareMode)
            {
                case Global.CompareMode.EQUAL:
                    return vel == Value;
                case Global.CompareMode.DIFFERENT:
                    return vel != Value;
                case Global.CompareMode.GREATER:
                    return vel > Value;
                case Global.CompareMode.GREATER_EQUAL:
                    return vel >= Value;
                case Global.CompareMode.LESS:
                    return vel < Value;
                case Global.CompareMode.LESS_EQUAL:
                    return vel <= Value;
            }

            return false;
        }
    }
}
