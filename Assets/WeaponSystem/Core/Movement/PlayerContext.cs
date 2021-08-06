using UnityEngine;
using WeaponSystem.Core.Runtime;

namespace WeaponSystem.Core.Movement
{
    public class PlayerContext : MonoBehaviour, IPlayerContext
    {
        private void OnEnable() => Locator<IPlayerContext>.Instance.Bind(this);

        private void OnDisable() => Locator<IPlayerContext>.Instance.Unbind(this);

        public bool IsMove { get; set; } = false;
        public bool IsGrounded { get; set; } = false;
        public bool IsSprint { get; set; } = false;
        public bool IsCrouch { get; set; } = false;

        public PlayerMovementState State
        {
            get
            {
                if (IsGrounded == false) return PlayerMovementState.Air;
                if (IsSprint == false) return PlayerMovementState.Sprint;
                if (IsMove) return PlayerMovementState.Walk;
                if (IsCrouch) return PlayerMovementState.Crouch;
                return PlayerMovementState.Rest;
            }
        }

        public bool IsAiming { get; set; }
    }
}