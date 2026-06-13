using UnityEngine;
using SakugaEngine.Resources;

namespace SakugaEngine
{
    public partial interface IDamage
    {
        void BaseDamage(SakugaActor target, HitboxElement box, Vector2Int contact);
        void HitTrade(SakugaActor target, HitboxElement box, Vector2Int contact);
        void ThrowDamage(SakugaActor target, HitboxElement box, Vector2Int contact);
        void ThrowTrade();
        void HitboxClash(SakugaActor target, HitboxElement box, Vector2Int contact);
        void ProjectileClash(SakugaActor target, HitboxElement box, Vector2Int contact);
        void ProjectileDeflect(SakugaActor target, HitboxElement box, Vector2Int contact);
        void CounterHit(SakugaActor target, HitboxElement box, Vector2Int contact);
        //void ParryHit(SakugaActor target, HitboxElement box, Vector2Int contact);
        void ProximityBlock(HitboxElement box);
        void OnHitboxExit();
    }
}