using System;
using UnityEngine;

namespace SakugaEngine.Resources
{
    [CreateAssetMenu(fileName = "New Fighter List", menuName = "Sakuga Engine/Fighter List", order = 0)]
    public partial class FighterList : ScriptableObject
    {
        public FighterElement[] elements;
    }
}

