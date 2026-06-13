using UnityEngine;
using System;
using SakugaEngine.Global;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class AddGravityAction : FrameDataAction
    {
        [SerializeField] private bool SetValue;
        [SerializeField] private bool UseDefault;
        [SerializeField] private int Force;

        public override void Execute(ref SakugaActor Actor)
        {
            if (Actor.Body == null) return;

            if (SetValue) Actor.Body.CurrentGravity = UseDefault ? GlobalVariables.DefaultGravity : Force;
            Actor.Body.AddGravity();
        }
    }
}
