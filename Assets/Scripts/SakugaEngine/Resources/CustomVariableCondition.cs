using SakugaEngine.Global;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class CustomVariableCondition
    {
        public int ByIndex;
        public string ByName = "";
        public CustomVariableMode Mode;
        public int Value;
        public CompareMode CompareMode;
    }
}
