using UnityEngine;
using System;
using SakugaEngine.Global;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class ForcedKnockbackAction : FrameDataAction
    {
        [SerializeField] private HitstunType HitstunType;
        [SerializeField] private int KnockbackHitReaction;
        [SerializeField] private int KnockbackTime;
        [SerializeField] private Vector2Int KnockbackDirection;
        [SerializeField] private int KnockbackGravity;
        [SerializeField] private int Hitstop = 0;
        [SerializeField] private int Hitstun = 8;
        [SerializeField] private int BounceTime = 0;
        [SerializeField] private int HorizontalBounceIntensity = 0;
        [SerializeField] private int VerticalBounceIntensity = 0;

        public override void Execute(ref SakugaActor Actor)
        {
            if (Actor.Body == null) return;

            Actor.HitstunType = HitstunType;

            if (Actor.CanHitstop() && Hitstop > 0) Actor.Hitstop.StartTimer((uint)Hitstop);
            if (Actor.CanHitstun() && Hitstun > 0) Actor.Hitstun.StartTimer((uint)Hitstun);

            Actor.StateManager.PlayState(Actor.StanceManager.GetCurrentStance().HitReactions[KnockbackHitReaction]);

            Actor.PushActor(
                KnockbackTime, 
                KnockbackDirection.x * Actor.Body.PlayerSide,
                KnockbackDirection.y, 
                KnockbackGravity
            );

            Actor.StartBounce((uint)BounceTime, HorizontalBounceIntensity, VerticalBounceIntensity);
        }
    }
}
