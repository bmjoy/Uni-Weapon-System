namespace WeaponSystem.Core.Movement
{
    public class NoneState : IPlayerState
    {
        public PlayerMovementState State => PlayerMovementState.Rest;
    }
}
