using UnityEngine;

namespace SakugaEngine.Resources
{
    [CreateAssetMenu(fileName = "New Game Mode", menuName = "Sakuga Engine/Game Mode", order = 0)]
    public partial class GameModeSettings : ScriptableObject
    {
        public string ModeName;
        public bool AutoStart;
        public Global.NetcodeMode NetcodeMode;
        public Global.PauseMode PauseMode;
        public bool UseLAN;
        public bool CanKO;
        public bool AutoFillResources;
        public bool ShowMatchIntro; // doesn't exist
        public bool ShowMatchCards; // doesn't exist
        public bool ShowTrainingInfo;
        public bool ShowOnlineInfo;
        //public bool ShowTimeline; // For replay mode (doesn't exist)
        //public bool ShowComboList; // For combo challenge mode (doesn't exist)
        public bool AllowShowHitboxes;
        public bool AllowShowInputs;
        public bool AllowShowDebugElements;
        public bool AllowUseDebugCommands;
        public bool NoAI;
        public string ReturnToScene;
    }
}
