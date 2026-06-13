using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class FrameDataEvent
    {
        public FrameDataCondition[] Conditions;
        public FrameDataAction[] Actions;

        public void RunEvent(ref SakugaActor Actor)
        {
            if (!CheckConditions(ref Actor)) return;

            ExecuteActions(ref Actor);
        }

        private bool CheckConditions(ref SakugaActor Actor)
        {
            if (Conditions == null || Conditions.Length == 0) return true;

            foreach(FrameDataCondition condition in Conditions)
            {
                if (!condition.Check(ref Actor))
                    return false;
            }

            return true;
        }

        private void ExecuteActions(ref SakugaActor Actor)
        {
            foreach(FrameDataAction action in Actions)
            {
                action.Execute(ref Actor);
            }
        }
    }
}
