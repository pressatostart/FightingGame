using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class InterruptMoveAction : FrameDataAction
    {
        public override void Execute(ref SakugaActor Actor)
        {
            if (Actor.StanceManager == null) return;

            Actor.StanceManager.CurrentMove = -1;
        }
    }
}
