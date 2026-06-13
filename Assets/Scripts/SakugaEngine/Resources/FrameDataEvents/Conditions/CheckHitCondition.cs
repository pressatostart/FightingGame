using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class CheckHitCondition : FrameDataCondition
    {
        [SerializeField] private Global.CancelCondition HitCheck;
        [SerializeField] private bool Not;

        public override bool Check(ref SakugaActor Actor)
        {
            if (Not) return (Actor.CancelConditions & HitCheck) == 0;
            return (Actor.CancelConditions & HitCheck) != 0;
        }
    }
}
