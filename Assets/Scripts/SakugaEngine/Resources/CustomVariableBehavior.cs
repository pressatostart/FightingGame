using System;
using SakugaEngine.Global;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class CustomVariableBehavior
    {
        public CustomVariableBehaviorTarget Target;
        public bool SetValue;
        public int Factor;
        public int Value;
        public bool IsRandom;
        public int Range;
        public CustomVariableMode Mode;
    }
}
