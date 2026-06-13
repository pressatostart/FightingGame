using UnityEngine;
using System;
using SakugaEngine.Game;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class RoundRunningCondition : FrameDataCondition
    {
        [SerializeField] private bool Not;

        public override bool Check(ref SakugaActor Actor)
        {
            if (GameManager.Instance == null) return false;

            if (Not) return GameManager.Instance.Monitor.MatchState != Global.MatchState.ROUND_RUNNING;
            return GameManager.Instance.Monitor.MatchState == Global.MatchState.ROUND_RUNNING;
        }
    }
}
