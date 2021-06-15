namespace WeaponSystem.Collision
{
    public interface IHitPoint
    {
        void AddDamage(float damage);
        void AddRecovery(float hitPoint);
        void Death();
    }
}