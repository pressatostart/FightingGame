using UnityEngine;
using SakugaEngine.Global;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class CustomVariableChange
    {
        public int ByIndex;
        public string ByName = "";
        public CustomVariableMode Mode;
        public int Value;
        public bool IsRandom;
        public int Range = -1;
    }
}