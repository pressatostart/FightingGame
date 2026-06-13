using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [CreateAssetMenu(fileName = "New BGM List", menuName = "Sakuga Engine/BGM List", order = 0)]
    public partial class BGMList : ScriptableObject
    {
        public BGMElement[] elements;
    }
}
