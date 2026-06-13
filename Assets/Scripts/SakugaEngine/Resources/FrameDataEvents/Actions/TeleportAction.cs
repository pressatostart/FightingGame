using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class TeleportAction : FrameDataAction
    {
        [SerializeField] private int Index;
        [SerializeField] private Vector2Int TargetPosition = Vector2Int.zero;
        [SerializeField] private Global.RelativeTo xRelativeTo, yRelativeTo;
        
        public override void Execute(ref SakugaActor Actor)
        {
            if (Actor.Body == null) return;
            
            Actor.Body.MoveTo(Actor.GenerateTargetPosition(TargetPosition, Index, xRelativeTo, yRelativeTo));
        }
    }
}
