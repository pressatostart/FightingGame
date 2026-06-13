using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class OnKnockbackCondition : FrameDataCondition
    {
        [SerializeField] private bool Not;

        public override bool Check(ref SakugaActor Actor)
        {
            if (!Actor.CanKnockback()) return false;

            if (Not) return !Actor.OnKnockback();
            return Actor.OnKnockback();
        }
    }
}
