using System;
using UnityEngine;
using WeaponSystem.Input;
using WeaponSystem.Movement;
using WeaponSystem.Runtime;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action.AttackAction
{
    [AddTypeMenu("Control/Composite")]
    [Serializable]
    public class CompositeAttackAction : IAttackAction
    {
        [SerializeReference, SubclassSelector] private IAttackAction[] _attackActionModes;
        private int _index;
        private Animator _animator;
        private static readonly int ModeChange = Animator.StringToHash("ModeChange");

        public void Injection(Transform parent, Animator animator, IMagazine magazine)
        {
            foreach (var attackActionMode in _attackActionModes)
            {
                attackActionMode.Injection(parent, animator, magazine);
            }
            _animator = animator;
        }

        public void Action(bool isAction, IPlayerContext context)
        {
            if (Locator<IWeaponInput>.Instance.Current?.IsModeChanged ?? false) OnModeChanged();
            _attackActionModes[_index].Action(isAction, context);
        }

        private void OnModeChanged()
        {
            _index = (_index + 1) % _attackActionModes.Length;
            _animator.SetTrigger(ModeChange);
        }
    }
}