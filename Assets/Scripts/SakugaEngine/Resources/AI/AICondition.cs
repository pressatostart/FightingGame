using UnityEngine;
using System;
using SakugaEngine.Global;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class AICondition
    {
        public Vector2Int Distance;
        public bool UseOnGround, UseOnAir;
        public int SuperGaugeRequired;
        public int Probability = 10;
        public BotMode ActionMode;
        public int[] ActionsList;
        public AIFlags CounterFlags;
    }
}
