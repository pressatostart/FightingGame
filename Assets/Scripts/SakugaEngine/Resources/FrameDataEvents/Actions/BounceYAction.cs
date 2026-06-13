using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class BounceYAction : FrameDataAction
    {
        [SerializeField] private bool CustomIntensity;
        [SerializeField] private int Intensity;

        public override void Execute(ref SakugaActor Actor)
        {
            if (CustomIntensity) Actor.BounceYIntensity = Intensity;

            Actor.Body.BounceY(Actor.BounceYIntensity);
            Actor.BounceYIntensity = 0;
            Actor.Bounce.Stop();
        }
    }
}
