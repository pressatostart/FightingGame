using UnityEngine;
using System;

namespace SakugaEngine.Resources
{    
    [CreateAssetMenu(fileName = "New Match Settings", menuName = "Sakuga Engine/New Match Settings", order = 0)]
    public class MatchSettings : ScriptableObject
    {
        [Header("Main Settings")]
        public GameModeSettings[] GameModes;
        public int SelectedMode;
        public int TimeLimit = -1;
        public int RoundsToWin = 2;
        public int SelectedStage;
        public int SelectedBGM;
        public Global.BotDifficulty Difficulty = Global.BotDifficulty.MEDIUM;
        [Header("Player 1")]
        public int P1SelectedCharacter = 0;
        public int P1SelectedColor = 0;
        public int P1SelectedDevice = -1; // -1 = CPU player
        [Header("Player 2")]
        public int P2SelectedCharacter = 0;
        public int P2SelectedColor = 0;
        public int P2SelectedDevice = -1; // -1 = CPU player
        [Header("Online Settings")]
        public string OnlineP1Name;
        public Sprite OnlineP1PlatformIcon;
        public string OnlineP2Name;
        public Sprite OnlineP2PlatformIcon;
        public bool IsSpectator;
        public int PlayerID;
        public int SpectatorCount;

        public GameModeSettings SelectedModeSettings => GameModes[SelectedMode];
    }
}
