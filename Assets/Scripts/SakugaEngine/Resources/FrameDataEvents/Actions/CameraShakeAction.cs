using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class CameraShakeAction : FrameDataAction
    {
        [SerializeField] public float Amplitude;
        [SerializeField] public float WaveLength;
        [SerializeField] public int Duration;
        [SerializeField] public bool SideRelative;
        [SerializeField] public Vector2 Direction;

        public override void Execute(ref SakugaActor Actor)
        {
            
        }
    }
}
