namespace WeaponSystem.Collision
{
    public interface IDamageable
    {
        HitType HitType { get; }
        IObjectGroup ObjectGroup { get; }
        void AddDamage(float damage);
    }
}