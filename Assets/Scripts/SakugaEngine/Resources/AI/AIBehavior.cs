using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class AIBehavior
    {
        [Header("Blackboard")]
        /// <summary>
        /// A random threshold on which the CPU can make decisions when idle.
        /// </summary>
        public Vector2Int DecisionRateFree;
        /// <summary>
        /// A random threshold on which the CPU can make decisions when doing an action.
        /// </summary>
        public Vector2Int DecisionRateBusy;
        /// <summary>
        /// A random threshold to CPU inputs.
        /// </summary>
        public Vector2Int InputRandomness;
        public int BlockingRate;
        public int TechingRate;
        public int PredictionQuality;
        /// <summary>
        /// If the character's health is smaller than this, the CPU will enter a defensive state.
        /// </summary>
        public int LowHealth = 4000;

        [Header("Behavior Tree")]
        public AIActionPack NearActions;
        public AIActionPack MidActions;
        public AIActionPack FarActions;
        public AIActionPack DistantActions;
    }
}
