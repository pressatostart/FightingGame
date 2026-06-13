using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class StopKnockbackAction : FrameDataAction
    {
        [SerializeField] private bool KeepXVelocity;

        public override void Execute(ref SakugaActor Actor)
        {
            if (Actor.Body == null) return;

            Actor.StopKnockback(KeepXVelocity);
        }
    }
}
