using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class FrameDataCondition
    {
        public virtual bool Check(ref SakugaActor Actor)
        {
            return Actor != null;
        }
    }
}
