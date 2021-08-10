using UnityEngine;
using WeaponSystem.Core.Runtime;

namespace WeaponSystem.Core.Movement
{
    public class DebugPlayerState : MonoBehaviour, IPlayerState
    {
        [SerializeField] private PlayerMovementState state;
        public PlayerMovementState State => state;

        private void Start() => Locator<IPlayerState>.Instance.Bind(this);
    }
}