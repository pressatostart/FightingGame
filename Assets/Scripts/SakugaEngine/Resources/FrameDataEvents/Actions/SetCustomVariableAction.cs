using UnityEngine;
using System;
using SakugaEngine.Global;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class SetCustomVariableAction : FrameDataAction
    {
        [SerializeField] private int ByIndex;
        [SerializeField] private string ByName;
        [SerializeField] public CustomVariableMode ChangeMode;
        [SerializeField] private int NewValue;
        [SerializeField] private bool IsRandom;
        [SerializeField] private int RandomRange;

        public override void Execute(ref SakugaActor Actor)
        {
            if (Actor.Parameters == null) return;
            CustomVariable variable = null;
            if (ByIndex >= 0) variable = Actor.Parameters.GetVariable(ByIndex);
            else if (ByName != "") variable = Actor.Parameters.GetVariable(ByName);
            if (variable == null) return;

            int finalValue = IsRandom ? RNG.Next(NewValue, RandomRange) : NewValue;

            switch (ChangeMode)
            {
                case CustomVariableMode.IDLE:
                    variable.Set(finalValue);
                    break;
                case CustomVariableMode.INCREASE:
                    variable.Add(finalValue);
                    break;
                case CustomVariableMode.DECREASE:
                    variable.Subtract(finalValue);
                    break;
            }
            
        }
    }
}
