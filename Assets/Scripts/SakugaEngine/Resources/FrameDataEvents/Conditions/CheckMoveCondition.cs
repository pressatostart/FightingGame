using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class CheckMoveCondition : FrameDataCondition
    {
        [SerializeField] private int CheckSpecificMove;
        [SerializeField] private bool Not;

        public override bool Check(ref SakugaActor Actor)
        {
            if (Actor.StanceManager == null) return false;

            if (CheckSpecificMove <= 0)
            {
                if (Not) return Actor.StanceManager.CurrentMove < 0;
                return Actor.StanceManager.CurrentMove >= 0;
            }

            if (Not) return Actor.StanceManager.CurrentMove != CheckSpecificMove;
            return Actor.StanceManager.CurrentMove == CheckSpecificMove;
        }
    }
}
