using System;
using UnityEngine;
using WeaponSystem.Input;
using WeaponSystem.Movement;
using WeaponSystem.Runtime;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action.AttackAction
{
    [AddTypeMenu("Attack/CompositeAttack")]
    [Serializable]
    public class CompositeAttackAction : IAttackAction
    {
        [SerializeReference, SubclassSelector] private IAttackAction[] _attackActionModes;
        private int _index;
        private Animator _animator;
        private static readonly int ModeChange = Animator.StringToHash("ModeChange");

        public void Injection(Transform parent, Animator animator, IMagazine magazine, IPlayerContext context)
        {
            foreach (var attackActionMode in _attackActionModes)
            {
                attackActionMode.Injection(parent, animator, magazine, context);
            }
            _animator = animator;
        }

        public void Action(bool isAction)
        {
            if (Locator<IWeaponInput>.Instance.Current?.IsModeChanged ?? false) OnModeChanged();
            _attackActionModes[_index].Action(isAction);
        }

        private void OnModeChanged()
        {
            _index = (_index + 1) % _attackActionModes.Length;
            _animator.SetTrigger(ModeChange);
        }
    }
}