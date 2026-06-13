using UnityEngine;
using SakugaEngine.Global;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class MoveCancelSettings
    {
        public int MoveIndex;
        public CancelCondition Conditions;
        public Vector2Int FrameThreshold;
    }
}