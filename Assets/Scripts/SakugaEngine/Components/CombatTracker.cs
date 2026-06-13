using UnityEngine;

namespace SakugaEngine
{
    public partial class CombatTracker : MonoBehaviour
    {
        private SakugaActor _owner;
        [HideInInspector] public int HitCombo;
        [HideInInspector] public int LastDamage;
        [HideInInspector] public int CurrentCombo;
        [HideInInspector] public int HighestCombo;
        [HideInInspector] public int TotalFrames;
        [HideInInspector] public int FrameData;
        [HideInInspector] public int FrameAdvantage;
        [HideInInspector] public int HitFrame;
        [HideInInspector] public int StunAtHit;
        [HideInInspector] public int LastHitType;
        [HideInInspector] public bool invalidHit;

        public void Initialize(SakugaActor onwer)
        {
            _owner = onwer;
            HitCombo = 0;
            LastDamage = 0;
            CurrentCombo = 0;
            HighestCombo = 0;
            TotalFrames = 0;
            FrameData = 0;
            FrameAdvantage = 0;
            HitFrame = 0;
            StunAtHit = 0;
            LastHitType = 0;
            invalidHit = false;
        }
        public void UpdateTrackers(int damage, int hitFrame, int hitStun, int hitType, bool isInvalid = false, bool isCombo = true)
        {
            if (isInvalid)
                invalidHit = true;

            if (HitCombo == 0) CurrentCombo = 0;
            if (isCombo) HitCombo++;
            LastDamage = damage;
            CurrentCombo += damage;
            if (CurrentCombo > HighestCombo)
                HighestCombo = CurrentCombo;

            HitFrame = hitFrame;
            StunAtHit = hitStun;
            LastHitType = hitType;
            FrameAdvantage = HitFrame - StunAtHit;
        }

        public void UpdateFrameData()
        {
            bool corectStateType = _owner.StateManager.CurrentStateType() == Global.StateType.HIT_REACTION || _owner.StateManager.CurrentStateType() == Global.StateType.BLOCKING;
            int selectFrameOrigin = corectStateType ? (int)_owner.Hitstun.TimeLeft :
                                    (_owner.StateManager.GetCurrentState().AnimationData.Duration - _owner.StateManager.CurrentStateFrame);

            FrameData = _owner.StateManager.CurrentStateType() <= Global.StateType.MOVEMENT ? 0 : selectFrameOrigin;
        }

        public void Reset()
        {
            HitCombo = 0;
            //LastDamage = 0;
            //CurrentCombo = 0;
            invalidHit = false;
        }
    }
}
