using System;
using UnityEngine;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class FrameDataAction
    {
        public virtual void Execute(ref SakugaActor Actor)
        {
            Debug.Log($"Executing action at {Actor.Data.Profile.FighterName}");
        }
    }
}
