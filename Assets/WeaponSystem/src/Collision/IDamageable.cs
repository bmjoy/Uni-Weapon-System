namespace WeaponSystem.Collision
{
    public interface IDamageable
    {
        HitType HitType { get; }
        void AddDamage(float damage);
    }
}