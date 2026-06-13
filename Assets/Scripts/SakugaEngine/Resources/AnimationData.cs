using System;
using UnityEngine;

namespace SakugaEngine.Resources
{
    [CreateAssetMenu(fileName = "New Animation Data", menuName = "Sakuga Engine/Animation Data", order = 0)]
    [Serializable]
    public class AnimationData : ScriptableObject
    {
        public int Duration;
        public int HitstunHold = -1;
        public AnimationSettings[] Animations;
        public HitboxState[] Hitboxes;
    }
}
