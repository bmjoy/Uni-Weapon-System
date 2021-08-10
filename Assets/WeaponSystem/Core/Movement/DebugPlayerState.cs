using UnityEngine;
using WeaponSystem.Core.Runtime;

namespace WeaponSystem.Core.Movement
{
    public class DebugPlayerState : MonoBehaviour, IPlayerState
    {
        [SerializeField] private PlayerMovementAction action;
        public PlayerMovementAction Action => action;

        private void Start() => Locator<IPlayerState>.Instance.Bind(this);
    }
}