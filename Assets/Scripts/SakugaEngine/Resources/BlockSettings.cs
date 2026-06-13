using System;
using SakugaEngine.Global;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class BlockSettings
    {
        public StanceSelect ReferenceStance;
        public BlockType BlockType;
        public MotionInputs InputToCheck;
        public int EnterState;
        public int StunState;
        public int ExitState;
        public int GuardCrushState;
    }
}