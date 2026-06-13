using SakugaEngine;
using UnityEngine;

namespace SakugaEngine.Resources
{
    [CreateAssetMenu(fileName = "New Data Container", menuName = "Sakuga Engine/Data Container", order = 0)]
    public class DataContainer : ScriptableObject
    {
        public FighterProfile Profile;
        public FighterState[] States;
        public FighterStance[] Stances;

        public int MaxHealth = 10000;
        public int MaxSuperGauge = 10000;
        public ushort BaseAttack = 100;
        public ushort BaseDefense = 100;

        public AIData _aiData;

        public FighterIntro[] Intros;
        public FighterOutro[] Outros;
        public FighterIntro[] VictoryPoses;
        public int DefeatPose;
    }
}
