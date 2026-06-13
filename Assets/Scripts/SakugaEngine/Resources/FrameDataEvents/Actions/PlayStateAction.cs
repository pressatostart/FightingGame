using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class PlayStateAction : FrameDataAction
    {
        [SerializeField] private int StateIndex;
        [SerializeField] private string StateName;
        [SerializeField] private bool Reset = true;
        public override void Execute(ref SakugaActor Actor)
        {
            if (Actor.StateManager == null) return;

            int nextState = StateIndex;
            if (StateIndex < 0 && StateName != "") nextState = Actor.StateManager.GetStateIndex(StateName);

            Actor.StateManager.PlayState(nextState, Reset);
        }
    }
}
