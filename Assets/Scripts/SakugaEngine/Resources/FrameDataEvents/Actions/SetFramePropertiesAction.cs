using UnityEngine;
using System;
using SakugaEngine.Global;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class SetFramePropertiesAction : FrameDataAction
    {
        [SerializeField] private FrameProperties Properties;
        [SerializeField] private ParameterChange ChangeMode;
        
        public override void Execute(ref SakugaActor Actor)
        {
            switch (ChangeMode)
            {
                case ParameterChange.SET:
                    Actor.FrameProperties = Properties;
                    break;
                case ParameterChange.ADD:
                    Actor.FrameProperties |= Properties;
                    break;
                case ParameterChange.SUBTRACT:
                    Actor.FrameProperties &= ~Properties;
                    break;
            }
            
        }
    }
}
