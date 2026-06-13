using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class SetKnockoutStateAction : FrameDataAction
    {
        [SerializeField] private bool Not;

        public override void Execute(ref SakugaActor Actor)
        {
            Debug.Log("Should be dead for good...");
        }
    }
}
