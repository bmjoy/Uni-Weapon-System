namespace WeaponSystem.Core.Movement
{
    public class NoneState : IPlayerState
    {
        public PlayerMovementAction Action => PlayerMovementAction.Rest;
    }
}
