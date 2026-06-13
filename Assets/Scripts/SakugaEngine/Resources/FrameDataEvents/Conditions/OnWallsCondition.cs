using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class OnWallsCondition : FrameDataCondition
    {
        [SerializeField] private bool Not;
        public override bool Check(ref SakugaActor Actor)
        {
            if (Actor.Body == null) return false;
            
            if (Not) return !Actor.Body.IsOnWall;
            return Actor.Body.IsOnWall;
        }
    }
}
