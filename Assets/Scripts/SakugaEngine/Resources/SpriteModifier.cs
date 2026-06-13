using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class SpriteModifier
    {
        public Texture2D Sprite;
        public Vector2 Offset = new Vector2(-56, -4);
        [Range(0, 360)] public int Rotation;
        public bool FlipX;
        public bool FlipY;
    }
}
