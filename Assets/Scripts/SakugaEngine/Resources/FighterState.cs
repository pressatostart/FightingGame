using UnityEngine;
using SakugaEngine.Global;

namespace SakugaEngine.Resources
{
    [CreateAssetMenu(fileName = "New Fighter State", menuName = "Sakuga Engine/Fighter State", order = 0)]
    public class FighterState : ScriptableObject
    {
        public string StateName;
        public AnimationData AnimationData;
        public MasterStance BaseStance;
        public StateType Type;
        public AIFlags AIFlags;

        public FrameDataEvent[] TransitionEvents;
        public FrameDataEvent[] OnEnterEvents;
        public FrameDataEvent[] OnTickEvents;
        public FrameDataEvent[] OnExitEvents;
        public FrameDataEvent[] OnHitConfirmEvents;
        public FrameDataEvent[] OnHitReactionEvents;
    }
}