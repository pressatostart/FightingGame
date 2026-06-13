using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class SetActiveAction : FrameDataAction
    {
        [SerializeField] private bool Active;
        public override void Execute(ref SakugaActor Actor)
        {
            Actor.IsActive = Active;
        }
    }
}
