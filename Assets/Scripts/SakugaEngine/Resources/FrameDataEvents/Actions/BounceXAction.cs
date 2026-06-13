using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class BounceXAction : FrameDataAction
    {
        [SerializeField] private bool CustomIntensity;
        [SerializeField] private int Intensity;

        public override void Execute(ref SakugaActor Actor)
        {
            if (CustomIntensity) Actor.BounceXIntensity = Intensity;

            Actor.Body.BounceX(Actor.BounceXIntensity);
            Actor.BounceXIntensity = 0;
            Actor.Bounce.Stop();
        }
    }
}
