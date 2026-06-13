using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class AtFrameCondition : FrameDataCondition
    {
        [SerializeField] private int Frame;
        [SerializeField] private int Modulo;
        [SerializeField] private bool Not;

        public override bool Check(ref SakugaActor Actor)
        {
            if (Actor.StateManager == null) return false;

            if (Modulo > 0)
            {
                if (Not) return Actor.StateManager.CurrentStateFrame % Modulo != Frame;
                return Actor.StateManager.CurrentStateFrame % Modulo == Frame;
            }

            if (Not) return Actor.StateManager.CurrentStateFrame != Frame;
            return Actor.StateManager.CurrentStateFrame == Frame;
        }
    }
}
