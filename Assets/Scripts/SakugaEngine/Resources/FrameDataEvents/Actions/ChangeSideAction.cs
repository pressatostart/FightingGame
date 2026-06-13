using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class ChangeSideAction : FrameDataAction
    {
        [SerializeField] private bool Forced;
        public override void Execute(ref SakugaActor Actor)
        {
            if (Actor.Body == null) return;

            if (Forced) Actor.Body.UpdateSide(!Actor.Body.IsLeftSide);

            Actor.Body.FlipSide();
        }
    }
}
