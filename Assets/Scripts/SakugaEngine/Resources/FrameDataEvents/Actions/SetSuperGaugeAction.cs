using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class SetSuperGaugeAction : FrameDataAction
    {
        [SerializeField] private int Value;
        [SerializeField] private bool Absolute;
        
        public override void Execute(ref SakugaActor Actor)
        {
            if (Value == 0) return;
            if (Actor.Parameters == null) return;
            if (Actor.Parameters.SuperGauge == null) return;
            
            if (Absolute)
            {
                Actor.Parameters.SuperGauge.SetSuperGauge(Mathf.Abs(Value));
                return;
            }
            
            if (Value > 0)
                Actor.Parameters.SuperGauge.AddSuperGauge(Mathf.Abs(Value));
            else if (Value < 0)
                Actor.Parameters.SuperGauge.SpendSuperGauge(Mathf.Abs(Value));
        }
    }
}
