using System;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Camera;
using WeaponSystem.Movement;
using WeaponSystem.Runtime;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action.AltAttackAction
{
    [Serializable, AddTypeMenu("ScopeAim")]
    public class ScopeAimAction : IAltAttackAction
    {
        [SerializeField] private float[] zoomMultiplyList = {.5f, .4f};
        [SerializeField] private float duration = .2f;
        public UnityEvent onScopeMultiplyChanged;

        private int _index;

        public void Injection(Transform parent, Animator animator, IMagazine magazine) { }

        public void Action(bool isAction, IPlayerContext context)
        {
            context.IsAiming = isAction;

            var scope = Locator<IScopeCamera>.Instance.Current;
            scope.IsActive = isAction;

            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift)) OnIndexChange();
        }

        private void OnIndexChange()
        {
            onScopeMultiplyChanged.Invoke();
            _index = _index++ % zoomMultiplyList.Length;
        }
    }
}