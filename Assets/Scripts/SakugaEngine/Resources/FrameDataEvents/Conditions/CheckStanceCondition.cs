using UnityEngine;
using System;


namespace SakugaEngine.Resources
{
    [Serializable]
    public class CheckStanceCondition : FrameDataCondition
    {
        [SerializeField] private int CurrentStance;
        [SerializeField] private bool Not;

        public override bool Check(ref SakugaActor Actor)
        {
            if (Actor.StanceManager == null) return false;

            if (Not) return Actor.StanceManager.CurrentStance != CurrentStance;
            return Actor.StanceManager.CurrentStance == CurrentStance;
        }
    }
}
