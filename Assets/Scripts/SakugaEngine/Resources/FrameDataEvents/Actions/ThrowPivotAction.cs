using UnityEngine;
using System;
using SakugaEngine.Game;
using SakugaEngine.Global;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class ThrowPivotAction : FrameDataAction
    {
        [SerializeField] private Vector2Int PivotPosition = Vector2Int.zero;
        [SerializeField] private int ThrowReaction = -1;
        [SerializeField] private bool EndThrow;

        public override void Execute(ref SakugaActor Actor)
        {
            if (Actor.CurrentGrabbedNodeID < 0) return;

            SakugaActor grabbedActor = GameManager.Instance.GetActor(Actor.CurrentGrabbedNodeID) as SakugaActor;

            if (grabbedActor == null) return;
            if (grabbedActor.Body == null) return;
            if (grabbedActor.HitstunType != HitstunType.GRABBED) return;

            int grabState = ThrowReaction >= 0 ? grabbedActor.StanceManager.GetCurrentStance().HitReactions[ThrowReaction] : -1;
            grabbedActor.StateManager.PlayState(grabState, false);
            grabbedActor.Body.SetVelocity(Vector2Int.zero);
            grabbedActor.Body.MoveTo(Actor.Body.FixedPosition + PivotPosition * new Vector2Int(Actor.Body.PlayerSide, 1));

            if (EndThrow) Actor.CurrentGrabbedNodeID = -1;
        }
    }
}
