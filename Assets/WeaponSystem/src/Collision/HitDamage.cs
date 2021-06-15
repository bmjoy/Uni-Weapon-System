using System;

namespace WeaponSystem.Collision
{
    [Serializable]
    public struct HitDamage
    {
        public static HitDamage None => new HitDamage(HitType.Object);

        public HitType hitType;
        public float damage;

        public HitDamage(float damage, HitType hitType)
        {
            this.damage = damage;
            this.hitType = hitType;
        }

        public HitDamage(HitType hitType, float damage = 0f)
        {
            this.damage = damage;
            this.hitType = hitType;
        }
    }
}