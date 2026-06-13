using System;
using UnityEngine;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class FighterOutro
    {
        public int StateIndex;
        public string ForOpponent = "";
        [TextArea] public string FinalMessage;
    }
}
