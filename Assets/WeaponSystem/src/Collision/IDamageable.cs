namespace WeaponSystem.Collision
{
    public interface IDamageable
    {
        DamageCollision DamageCollision { get; }
        HitType HitType { get; }
        void AddDamage(float damage);
    }
}