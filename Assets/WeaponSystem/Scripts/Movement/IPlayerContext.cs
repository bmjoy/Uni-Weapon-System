namespace WeaponSystem.Scripts.Movement
{
    public interface IPlayerContext
    {
        PlayerMovementState State { get; }
        public bool IsAiming { get; set; }
    }
}