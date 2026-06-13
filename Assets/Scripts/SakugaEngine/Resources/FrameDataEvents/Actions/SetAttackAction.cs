using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class SetAttackAction : FrameDataAction
    {
        [SerializeField] private bool Reset;
        [SerializeField] private int Value;

        public override void Execute(ref SakugaActor Actor)
        {
            if (Actor.Parameters == null) return;
            if (Actor.Parameters.Prorations == null) return;

            if (Reset) Actor.Parameters.Prorations.CurrentAttack = Actor.Parameters.Prorations.BaseAttack;
            else Actor.Parameters.Prorations.CurrentAttack = (ushort)Value;
        }
    }
}
