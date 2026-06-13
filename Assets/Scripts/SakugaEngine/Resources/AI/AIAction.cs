using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class AIAction
    {
        [SerializeField] private string InputName; // For identification purposes
        public AIInput[] Inputs;
        public bool AutoAdvance;
    }
}