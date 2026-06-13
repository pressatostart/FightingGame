using UnityEngine;
using SakugaEngine.Collision;
using SakugaEngine.Game;
using SakugaEngine.Global;

namespace SakugaEngine.Utils
{
    public partial class HitboxViewer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] hitboxGraphics;
        [SerializeField] private PhysicsBody body;

        public void Update()
        {
            if (body == null) return;

            gameObject.SetActive(GameManager.Instance != null && GameManager.Instance.ShowHitboxes);
            transform.position = GlobalFunctions.ToScaledVector3(body.FixedPosition);
            transform.rotation = Quaternion.identity;

            UpdateHitboxes();
            UpdatePushbox();
        }

        public void UpdateHitboxes()
        {
            for(int j = 0; j < hitboxGraphics.Length; j++)
            {
                if (body.CurrentHitbox == null || body.CurrentHitbox.HitboxData == null || body.CurrentHitbox.HitboxData.Hitboxes == null || j >= body.CurrentHitbox.HitboxData.Hitboxes.Length)
                    hitboxGraphics[j].gameObject.SetActive(false);
                else
                {
                    hitboxGraphics[j].gameObject.SetActive(body.Hitboxes[j].Active);

                    switch (body.CurrentHitbox.HitboxData.Hitboxes[j].HitboxType)
                    {
                        case HitboxType.HURTBOX:
                            hitboxGraphics[j].sortingOrder = 1;
                            hitboxGraphics[j].color = new Color(0.0f, 1.0f, 0.0f);
                            break;
                        case HitboxType.HITBOX:
                            hitboxGraphics[j].sortingOrder = 2;
                            hitboxGraphics[j].color = new Color(1.0f, 0.0f, 0.0f);
                            break;
                        case HitboxType.PROJECTILE:
                            hitboxGraphics[j].sortingOrder = 2;
                            hitboxGraphics[j].color = new Color(1.0f, 0.64f, 0.0f);
                            break;
                        case HitboxType.PROXIMITY_BLOCK:
                            hitboxGraphics[j].sortingOrder = 4;
                            hitboxGraphics[j].color = new Color(1.0f, 0.0f, 1.0f);
                            break;
                        case HitboxType.THROW:
                            hitboxGraphics[j].sortingOrder = 2;
                            hitboxGraphics[j].color = new Color(0.0f, 0.0f, 1.0f);
                            break;
                        case HitboxType.COUNTER:
                            hitboxGraphics[j].sortingOrder = 2;
                            hitboxGraphics[j].color = new Color(0.5f, 0.5f, 0.5f);
                            break;
                        case HitboxType.DEFLECT:
                            hitboxGraphics[j].sortingOrder = 2;
                            hitboxGraphics[j].color = new Color(1.0f, 0.0f, 0.5f);
                            break;
                        /*case Global.HitboxType.PARRY:
                            hitboxGraphics[j].SortingOffset = 2;
                            hitboxGraphics[j].Modulate = new Color(0.5f, 0.5f, 0.5f);
                            break;*/
                    }
                    hitboxGraphics[j].transform.position = GlobalFunctions.ToScaledVector3(body.Hitboxes[j].Center);
                    hitboxGraphics[j].size = GlobalFunctions.ToScaledVector3(body.Hitboxes[j].Size, 1f);
                }
            }
        }

        public void UpdatePushbox()
        {
            int collisionViewer = hitboxGraphics.Length - 1;
            hitboxGraphics[collisionViewer].gameObject.SetActive(body.Pushbox.Active);
            hitboxGraphics[collisionViewer].sortingOrder = 3;
            hitboxGraphics[collisionViewer].color = new Color(1.0f, 1.0f, 0.0f);
            hitboxGraphics[collisionViewer].transform.position = GlobalFunctions.ToScaledVector3(body.Pushbox.Center);
            hitboxGraphics[collisionViewer].size = GlobalFunctions.ToScaledVector3(body.Pushbox.Size, 1f);
        }
    }
}
