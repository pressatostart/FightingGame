using System;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class ResetProrationsAction : FrameDataAction
	{
		public override void Execute(ref SakugaActor Actor)
		{
			if (Actor.Parameters == null) return;
			if (Actor.Parameters.Prorations == null) return;

			Actor.Parameters.Prorations.ResetProrations();
		}
	}
}