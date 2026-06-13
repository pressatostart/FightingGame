using UnityEngine;
using System;
using SakugaEngine.Game;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class CallNotificationAction : FrameDataAction
    {
        [SerializeField] private int Index;

        public override void Execute(ref SakugaActor Actor)
        {
            if (GameManager.Instance == null) return;

            GameManager.Instance.PlayHitNotificationByIndex((int)Actor.playerID, Index);
        }
    }
}
