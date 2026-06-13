using UnityEngine;
using SakugaEngine.Global;

namespace SakugaEngine.Resources
{
    [CreateAssetMenu(fileName = "New Move", menuName = "Sakuga Engine/Move Settings", order = 0)]
    public partial class MoveSettings : ScriptableObject
    {
        [Header("Move Parameters")]
        public string MoveName;
        public int MoveState;
        public InputSideCheck SideCheck = InputSideCheck.CHARACTER_RELATIVE;
        public MotionInputs Inputs;
        public int FrameLimit = -1;
        public int SuperFlash = 0;
        public int Priority = 0;
        public MoveEndCondition MoveEnd;
        public int MoveEndState = -1;

        [Header("Move Conditions")]
        public int SuperGaugeRequired = 0;
        public Vector2Int HealthRequired = new Vector2Int(0, 99999);
        public Vector2Int DistanceArea = new Vector2Int(0, 999999);
        public int MinimumHeight = 0;
        public int[] IsSequenceFromStates;
        public int[] IgnoreStates;
        public MoveCancelSettings[] CanCancelTo;
        public CustomVariableCondition[] VariablesRequirement;
        public CustomVariableChange[] VariablesChange;
        
        [Header("Move Flags")]
        public bool SkipCheck;
        public bool Unlockable;
        public bool CanBeOverrided;
        public bool IgnoreSelf = true;
        public bool PriorityBuffer;
        public bool IgnoreSamePriority = true;
        public bool AcceptNullStates = false;
        public bool AcceptMovementStates = true;
        public bool AcceptCombatStates = false;
        public bool AcceptBlockingStates = false;
        public bool AcceptHitReactionStates = false;
        public bool IgnoreHitstop = false;
        public bool IgnoreHitstun = false;
        public bool IgnoreKnockdownHits = false;
        public bool UseOnGround, UseOnAir;
    }
}
