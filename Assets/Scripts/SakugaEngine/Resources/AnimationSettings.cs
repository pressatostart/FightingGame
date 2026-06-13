using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class AnimationSettings
    {
        public int AtFrame;
        public string SourceAnimation;
        public float Speed = 1f;
        public bool Inverse = false;
        public int FrameStepping = 1;
        public bool LimitRange;
        public Vector2Int AnimationRange;
        public Vector3 Offset = Vector3.zero;
    }
}
