using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class CallStanceAction : FrameDataAction
    {
        [SerializeField] private int NextStance;

        public override void Execute(ref SakugaActor Actor)
        {
            if (Actor.StanceManager == null) return;

            Actor.StanceManager.CallStance(NextStance);
        }
    }
}
