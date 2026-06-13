using UnityEngine;
using System;
using SakugaEngine.Global;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class CheckDistanceCondition : FrameDataCondition
    {
        [SerializeField] private int MinDistance;
        [SerializeField] private int MaxDistance;
        [SerializeField] private RelativeTo RelativeTo;
        [SerializeField] private int Index;
        [SerializeField] private bool Not;
        
        public override bool Check(ref SakugaActor Actor)
        {
            if (Actor.Body == null) return false;

            int dist = GlobalFunctions.HorizontalDistance(Actor.Body.FixedPosition, Actor.GenerateTargetPosition(Vector2Int.zero, Index, RelativeTo, RelativeTo));

            if (Not) return dist < MinDistance || dist > MaxDistance;
            return dist >= MinDistance && dist <= MaxDistance;
        }
    }
}
