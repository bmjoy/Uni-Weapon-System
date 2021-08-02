using UnityEngine;
using WeaponSystem.Core.Input;
using WeaponSystem.Core.Runtime;

namespace WeaponSystem.Core.Movement
{
    public class WalkerInput : MonoBehaviour
    {
        [SerializeField] private float walkSpeed;
        [SerializeField] private float sprintSpeed;
    
        private SimpleWalker _simpleWalker;

        private void Start() => _simpleWalker = GetComponent<SimpleWalker>();

        private void Update()
        {
            var input = Locator<IMovementInput>.Instance.Current;
            _simpleWalker.Direction = new Vector3(input?.Horizontal ?? 0f, 0f, input?.Vertical ?? 0f).normalized;
            _simpleWalker.IsCrouch = input?.IsCrouch ?? false;
            _simpleWalker.IsJump = input?.IsJump ?? false;
            _simpleWalker.Speed = input?.IsSprint ?? false ? sprintSpeed : walkSpeed;
        }
    }
}