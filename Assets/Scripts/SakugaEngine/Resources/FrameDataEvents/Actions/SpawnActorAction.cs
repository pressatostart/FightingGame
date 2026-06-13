using UnityEngine;
using System;
using SakugaEngine.Global;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class SpawnActorAction : FrameDataAction
    {
        [SerializeField] private Vector2Int TargetPosition = Vector2Int.zero;
        [SerializeField] private RelativeTo xRelativeTo, yRelativeTo;
        [SerializeField] private int Index;
        [SerializeField] private bool IsRandom;
        [SerializeField] private int Range;
        [SerializeField] private string Key;
        [SerializeField] private int FromExtraVariable = -1;
        [SerializeField] private int InitialState;
        
        public override void Execute(ref SakugaActor Actor)
        {
            if (Actor.Pool == null) return;
            
            Vector2Int origin = Actor.GenerateTargetPosition(TargetPosition, Index, xRelativeTo, yRelativeTo);
                            
            int ind = IsRandom ? RNG.Next(Index, Range) : Index;
            if (FromExtraVariable >= 0)
            {
                ind = Actor.Parameters.Variables[FromExtraVariable].CurrentValue;
                Actor.Parameters.Variables[FromExtraVariable].ChangeBehavior(CustomVariableBehaviorTarget.ON_USE);
            }
            
            if (ind >= 0)
                Actor.Pool.SpawnObject(ind, InitialState, origin);
            else if (Key != "")
                Actor.Pool.SpawnObject(Key, InitialState, origin);
        }
    }
}
