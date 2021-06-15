namespace WeaponSystem.Movement
{
    public class NoneContext : IPlayerContext
    {
        public PlayerMovementState State => PlayerMovementState.Rest;
        public bool IsAiming { get; set; }
    }
}
