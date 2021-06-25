namespace WeaponSystem.Collision
{
    public interface IHasHitPoint
    {
        void AddDamage(float damage);
        void AddRecovery(float hitPoint);
        void Death();
    }
}