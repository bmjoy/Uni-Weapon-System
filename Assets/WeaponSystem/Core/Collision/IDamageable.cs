namespace WeaponSystem.Core.Collision
{
    public interface IDamageable
    {
        BodyType BodyType { get; }
        IObjectGroup ObjectGroup { get; }
        void AddDamage(float damage);
    }
}