using UnityEngine;
using System;
using SakugaEngine.Global;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class RandomNumberCondition : FrameDataCondition
    {
        [SerializeField] private Vector2Int Range = new(0, int.MaxValue);
        [SerializeField] private int Value;
        [SerializeField] private bool Not;

        public override bool Check(ref SakugaActor Actor)
        {
            int random = RNG.Next(Range.x, Range.y);

            if (Not) return random != Value;
            return random == Value;
        }
    }
}
