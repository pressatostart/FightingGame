using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class ResetHitAction : FrameDataAction
    {
        public override void Execute(ref SakugaActor Actor)
        {
            if (Actor.Body == null) return;

            Actor.Body.ResetHits();
        }
    }
}
