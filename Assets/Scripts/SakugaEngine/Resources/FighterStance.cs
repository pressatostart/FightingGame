using System;
using UnityEngine;

namespace SakugaEngine.Resources
{
    [CreateAssetMenu(fileName = "New Fighter Stance", menuName = "Sakuga Engine/Fighter Stance", order = 0)]
    public partial class FighterStance : ScriptableObject
    {
        [Header("Settings")]
        public bool IsDamagePersistent;
        public bool IsRoundPersistent;
        public int DefaultState = 0;
        public MoveSettings[] Moves;
        public int[] HitReactions;
        public BlockSettings[] BlockReactions;

        [Header("Throw Escape")]
        public MotionInputs ThrowEscapeInput;
        public int GroundThrowEscapeState = -1;
        public int AirThrowEscapeState = -1;
    }
}
