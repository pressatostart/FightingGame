using UnityEngine;
using System;
using SakugaEngine.Global;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class PlayAnnouncerLineAction : FrameDataAction
    {
        [SerializeField] public int Index;

        public override void Execute(ref SakugaActor Actor)
        {
            if (AudioManager.Instance == null) return;
            
            AudioManager.Instance.PlayAnnouncerClip(Index);
        }
    }
}
