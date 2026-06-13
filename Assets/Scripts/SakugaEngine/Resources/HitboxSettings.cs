using System;
using UnityEngine;

namespace SakugaEngine.Resources
{
    [CreateAssetMenu(fileName = "New Hitbox Settings", menuName = "Sakuga Engine/Hitbox Settings", order = 0)]
    public class HitboxSettings : ScriptableObject
    {
        public BoxShape Pushbox;
        public HitboxElement[] Hitboxes;
    }
}
