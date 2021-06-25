using System;
using UnityEngine;
using WeaponSystem.Movement;
using WeaponSystem.Runtime;

namespace WeaponSystem
{
    [Serializable]
    public class PlayerMoveBaseContext : MonoBehaviour, IPlayerContext
    {
        [SerializeField] private float restSpeedThreshold = .1f;
        [SerializeField] private float walkSpeedThreshold = 6f;
        public PlayerMovementState State => _state;
        private PlayerMovementState _state;
        public bool IsAiming { get; set; }
        
        public float Speed { get; set; }
        public bool IsCrouch { get; set; }
        public bool IsGrounded { get; set; }
        
        private void OnEnable() => Locator<IPlayerContext>.Instance.Bind(this);

        private void OnDisable() => Locator<IPlayerContext>.Instance.Unbind(this);

        private void Update()
        {
            if (IsGrounded == false)
            {
                _state = PlayerMovementState.Air;
                return;
            }

            if (IsCrouch)
            {
                _state = PlayerMovementState.Crouch;
                return;
            }

            if (Speed > walkSpeedThreshold)
            {
                _state = PlayerMovementState.Sprint;
            }

            if (Speed > restSpeedThreshold)
            {
                _state = PlayerMovementState.Walk;
            }

            _state = PlayerMovementState.Rest;
        }
    }
}