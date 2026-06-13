using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class AIInput
    {
        public Global.DirectionalInputs Direction = Global.DirectionalInputs.NEUTRAL;
        public Global.ButtonInputs Buttons = Global.ButtonInputs.NEUTRAL;
    }
}
