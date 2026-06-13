using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [CreateAssetMenu(fileName = "New AI Data", menuName = "Sakuga Engine/AI Data", order = 0)]
    public class AIData : ScriptableObject
    {
        [Header("Actions")]
        public AIAction[] Actions;
        public int HighBlockAction;
        public int LowBlockAction;
        public int ForwardRecoveryAction;
        public int BackRecoveryAction;
        public int ThrowEscapeAction;

        [Header("Behaviors")]
        public AIBehavior BehaviorBeginner;
        public AIBehavior BehaviorEasy;
        public AIBehavior BehaviorMedium;
        public AIBehavior BehaviorHard;
        public AIBehavior BehaviorVeryHard;
        public AIBehavior BehaviorPro;
    }
}
