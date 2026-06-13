using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class SetDefenseAction : FrameDataAction
    {
        [SerializeField] private bool Reset;
        [SerializeField] private int Value;

        public override void Execute(ref SakugaActor Actor)
        {
            if (Actor.Parameters == null) return;
            if (Actor.Parameters.Prorations == null) return;

            if (Reset) Actor.Parameters.Prorations.CurrentDefense = Actor.Parameters.Prorations.BaseDefense;
            else Actor.Parameters.Prorations.CurrentDefense = (ushort)Value;
        }
    }
}