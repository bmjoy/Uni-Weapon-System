using UnityEngine;
using WeaponSystem.Movement;
using WeaponSystem.Runtime;

namespace WeaponSystem
{
    [System.Serializable]
    public class PlayerInputContext : MonoBehaviour, IPlayerContext
    {
        public PlayerMovementState State => _state;
        private PlayerMovementState _state;
        public bool IsAiming { get; set; }

        private void OnEnable() => Locator<IPlayerContext>.Instance.Bind(this);

        private void OnDisable() => Locator<IPlayerContext>.Instance.Unbind(this);
    }
}