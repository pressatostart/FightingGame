using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class SetArmorAction : FrameDataAction
    {
        [SerializeField] private int Value;
        public override void Execute(ref SakugaActor Actor)
        {
            if (Value == 0) return;
            if (Actor.Parameters == null) return;
            if (Actor.Parameters.SuperArmor == null) return;

            Actor.Parameters.SuperArmor.SetArmor(Value);
        }
    }
}
