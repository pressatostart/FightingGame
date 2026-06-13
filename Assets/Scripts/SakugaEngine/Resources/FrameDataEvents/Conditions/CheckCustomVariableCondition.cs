using UnityEngine;
using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class CheckCustomVariableCondition : FrameDataCondition
    {
        [SerializeField] private int ByIndex;
        [SerializeField] private string ByName;
        [SerializeField] private int Value;
        [SerializeField] public Global.CustomVariableMode Mode;
        [SerializeField] public Global.CompareMode CompareMode;

        public override bool Check(ref SakugaActor Actor)
        {
            if (Actor.Parameters == null) return false;
            CustomVariable variable = null;
            if (ByIndex >=0) variable = Actor.Parameters.GetVariable(ByIndex);
            else if (ByName != "") variable = Actor.Parameters.GetVariable(ByName);
            if (variable == null) return false;

            bool compareValue = variable.CompareValue(Value, CompareMode);
            bool compareMode = variable.CurrentMode == Mode;

            return compareValue && compareMode;
        }
    }
}
