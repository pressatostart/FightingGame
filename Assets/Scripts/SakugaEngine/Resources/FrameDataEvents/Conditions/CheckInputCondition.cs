using UnityEngine;
using System;
using SakugaEngine.Global;

namespace SakugaEngine.Resources
{
    [SerializeField]
    public class CheckInputCondition : FrameDataCondition
    {
        [SerializeField] private InputSideCheck SideCheck;
        [SerializeField] private MotionInputs Input;
        [SerializeField] private bool Not;
        
        public override bool Check(ref SakugaActor Actor)
        {
            if (Actor.Inputs == null) return false;
            
            if (Not) return !Actor.Inputs.CheckMotionInputs(Input, Actor.InputSide(SideCheck));
            return Actor.Inputs.CheckMotionInputs(Input, Actor.InputSide(SideCheck));
        }
    }
}
