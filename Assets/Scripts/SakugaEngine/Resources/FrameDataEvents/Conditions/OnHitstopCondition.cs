using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class OnHitstopCondition : FrameDataCondition
    {
        [SerializeField] private bool Not;

        public override bool Check(ref SakugaActor Actor)
        {
            if (!Actor.CanHitstop()) return false;

            if (Not) return !Actor.OnHitstop();
            return Actor.OnHitstop();
        }
    }
}
