using UnityEngine;
using SakugaEngine.Global;
using SakugaEngine.Resources;

namespace SakugaEngine
{
    [ExecuteInEditMode]
    public partial class AnimationViewer : MonoBehaviour
    {
        [SerializeField] private Animator[] players;
        [SerializeField] private string[] prefix;
        [SerializeField] private  Transform OffsetTransform;

        private int targetFrame = 0;

        public void SetDepth(int LayerSorting)
        {
            // TODO
        }

        public void ViewAnimations(AnimationSettings anim, int Frame)
        {
            if (anim == null) return;
            if (anim.SourceAnimation == "") return;

            int modTarget = (Frame - anim.AtFrame) % anim.FrameStepping;

            if (anim.LimitRange)
            {
                targetFrame = (Frame - anim.AtFrame) + anim.AnimationRange.x;
                targetFrame = Mathf.Clamp(targetFrame, anim.AnimationRange.x, anim.AnimationRange.y);
                if (anim.AnimationRange.y > anim.AnimationRange.x)
                    targetFrame -= modTarget;
            }
            else
            {
                targetFrame = Frame - anim.AtFrame;
                targetFrame -= modTarget;
            }

            if (anim.Inverse)
                targetFrame = anim.AnimationRange.y - targetFrame;

            for (int a = 0; a < players.Length; a++)
            {
                string prfx = "";
                if (prefix != null && prefix.Length > a) prfx = prefix[a];
                players[a].PlayInFixedTime(prfx + anim.SourceAnimation, 0, (targetFrame * anim.Speed) / (float)GlobalVariables.TicksPerSecond);
            }

            if (OffsetTransform != null)
                OffsetTransform.localPosition = anim.Offset;
        }
    }
}
