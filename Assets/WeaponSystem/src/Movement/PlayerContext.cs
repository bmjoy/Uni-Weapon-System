using WeaponSystem.Movement;

namespace WeaponSystem
{
    [System.Serializable]
    public class PlayerInputContext : IPlayerContext
    {
        public PlayerMovementState State { get; }

        public bool IsAiming
        {
            get => isAiming;
            set => isAiming = value;
        }

        public PlayerMovementState movementState;
        public bool isAiming;

        public static PlayerInputContext Default => new PlayerInputContext(PlayerMovementState.Rest, false);

        public PlayerInputContext(PlayerMovementState movementState, bool isAiming)
        {
            this.movementState = movementState;
            this.isAiming = isAiming;
        }
    }
}