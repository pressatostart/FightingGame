using UnityEngine;
using PleaseResync;

namespace SakugaEngine.Game
{
    public partial class SakugaRollbackManager : PleaseResyncManager
    {
        [SerializeField] private GameManager GameManager;

        public void Start()
        {
            GameManager.InputSize = InputSize;
            UseLAN = GameManager.Match.SelectedModeSettings.UseLAN;

            Debug.Log($"Starting {GameManager.Match.SelectedModeSettings.ModeName} mode.");

            if (GameManager.Match.SelectedModeSettings.AutoStart)
            {
                switch(GameManager.Match.SelectedModeSettings.NetcodeMode)
                {
                    case Global.NetcodeMode.LOCAL:
                        LocalGame(2);
                        break;
                    case Global.NetcodeMode.ONLINE:
                        OnlineGame(GameManager.Match.IsSpectator, 2, (uint)GameManager.Match.SpectatorCount, (uint)GameManager.Match.PlayerID);
                        break;
                    case Global.NetcodeMode.REPLAY:
                        ReplayMode(2);
                        break;
                }
            }
        }

        public override void OnlineGame(bool spectate, uint players, uint spectators, uint ID)
        {
            StartOnlineGame(GameManager, spectate, players, spectators, ID);
        }

        public override void LocalGame(uint players)
        {
            StartOfflineGame(GameManager, players);
        }

        public override void ReplayMode(uint players)
        {
            StartReplay(GameManager, players);
        }
    }
}
