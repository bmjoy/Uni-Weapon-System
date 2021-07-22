using UnityEngine;
using WeaponSystem.Runtime;

namespace WeaponSystem.Scripts.Movement
{
    public class PlayerContext : MonoBehaviour, IPlayerContext
    {
        [SerializeField] private float restSpeedThreshold = .1f;
        [SerializeField] private float walkSpeedThreshold = 6f;
        public PlayerMovementState State => state;
        [SerializeField] private PlayerMovementState state;
        [SerializeField] private bool grounded;

        public bool IsAiming { get; set; }

        public float Speed { get; set; }
        public bool IsCrouch { get; set; }

        public bool IsGrounded
        {
            get => grounded;
            set => grounded = value;
        }

        private void OnEnable() => Locator<IPlayerContext>.Instance.Bind(this);

        private void OnDisable() => Locator<IPlayerContext>.Instance.Unbind(this);

        private void Update()
        {
            if (IsGrounded == false)
            {
                state = PlayerMovementState.Air;
                return;
            }

            if (IsCrouch)
            {
                state = PlayerMovementState.Crouch;
                return;
            }

            if (Speed > walkSpeedThreshold)
            {
                state = PlayerMovementState.Sprint;
                return;
            }

            if (Speed > restSpeedThreshold)
            {
                state = PlayerMovementState.Walk;
                return;
            }

            state = PlayerMovementState.Rest;
        }
    }
}