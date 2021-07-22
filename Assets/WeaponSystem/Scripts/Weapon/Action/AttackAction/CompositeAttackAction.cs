using System;
using UnityEngine;
using WeaponSystem.Input;
using WeaponSystem.Runtime;
using WeaponSystem.Scripts.Movement;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action.AttackAction
{
    [AddTypeMenu("Control/Selectable")]
    [Serializable]
    public class SelectableAction : IAttackAction
    {
        [SerializeReference, SubclassSelector] private IAttackAction[] _attackActionModes;
        [SerializeField] private string modeChangeAnimParam = "isModeChange";
        private int _index;
        private Animator _animator;
        private int _modeChangeHash;

        public void Injection(Transform parent, Animator animator, IMagazine magazine)
        {
            foreach (var attackActionMode in _attackActionModes)
            {
                attackActionMode.Injection(parent, animator, magazine);
            }

            _modeChangeHash = Animator.StringToHash(modeChangeAnimParam);
            _animator = animator;
        }

        public void Action(bool isAction, IPlayerContext context)
        {
            if (Locator<IWeaponInput>.Instance.Current?.IsModeChanged ?? false) OnModeChanged();
            _attackActionModes[_index].Action(isAction, context);
        }

        private void OnModeChanged()
        {
            _index = ++_index % _attackActionModes.Length;
            _animator.SetBool(_modeChangeHash, true);
            _animator.SetBool(_modeChangeHash, false);
        }
    }
}