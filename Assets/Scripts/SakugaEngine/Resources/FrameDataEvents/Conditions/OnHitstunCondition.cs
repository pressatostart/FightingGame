using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public partial class OnHitstunCondition : FrameDataCondition
    {
        [SerializeField] private bool Not;

        public override bool Check(ref SakugaActor Actor)
        {
            if (!Actor.CanHitstun()) return false;

            if (Not) return !Actor.OnHitstun();
            return Actor.OnHitstun();
        }
    }
}
