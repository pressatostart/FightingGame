using UnityEngine;
using System;
using SakugaEngine.Global;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class PlaySoundAction : FrameDataAction
    {
        [SerializeField] private SoundType SoundType;
        [SerializeField] private int Source;
        [SerializeField] private int Index;
        [SerializeField] private bool IsRandom;
        [SerializeField] private int Range;
        [SerializeField] private int FromExtraVariable = -1;
        public override void Execute(ref SakugaActor Actor)
        {
            if (Actor.Parameters.SoundSources == null || Actor.Parameters.SoundSources.Length == 0) return;
            if (Actor.SFXList == null) return;
            if (Actor.VoiceLines == null) return;
            
            int ind = IsRandom ? RNG.Next(Index, Range) : Index;
            if (FromExtraVariable >= 0)
            {
                ind = Actor.Parameters.Variables[FromExtraVariable].CurrentValue;
                Actor.Parameters.Variables[FromExtraVariable].ChangeBehavior(CustomVariableBehaviorTarget.ON_USE);
            }
            AudioClip selectedSound = null;
            switch (SoundType)
            {
                case SoundType.SFX:
                    selectedSound = Actor.SFXList.Sounds[ind];
                    break;
                case SoundType.VOICE:
                    selectedSound = Actor.VoiceLines.Sounds[ind];
                    break;
            }
            Actor.Parameters.SoundSources[Source].QueueSound(selectedSound);                
        }
    }
}
