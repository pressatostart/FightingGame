using System;
using UnityEngine;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class HitboxState
    {
        public int AtFrame;
        public Global.AnimationStage animationStage;
        public HitboxSettings HitboxData;
    }
}
