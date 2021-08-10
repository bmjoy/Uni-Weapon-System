using UnityEngine;
using WeaponSystem.Core.Runtime;

namespace WeaponSystem.Core.Movement
{
    public class PlayerState : MonoBehaviour, IPlayerState
    {
        private void OnEnable() => Locator<IPlayerState>.Instance.Bind(this);

        private void OnDisable() => Locator<IPlayerState>.Instance.Unbind(this);

        public bool IsMove { get; set; } = false;
        public bool IsGrounded { get; set; } = false;
        public bool IsSprint { get; set; } = false;
        public bool IsCrouch { get; set; } = false;

        public PlayerMovementAction Action
        {
            get
            {
                if (IsGrounded == false) return PlayerMovementAction.Air;
                if (IsSprint == false) return PlayerMovementAction.Sprint;
                if (IsMove) return PlayerMovementAction.Walk;
                if (IsCrouch) return PlayerMovementAction.Crouch;
                return PlayerMovementAction.Rest;
            }
        }

        public bool IsAiming { get; set; }
    }
}