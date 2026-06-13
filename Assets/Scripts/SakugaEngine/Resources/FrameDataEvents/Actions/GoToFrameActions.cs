using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class GoToFrameActions : FrameDataAction
    {
        [SerializeField] private int Frame;
        public override void Execute(ref SakugaActor Actor)
        {
            if (Actor.StateManager == null) return;
            
            Actor.StateManager.CurrentStateFrame = Frame;
        }
    }
}
