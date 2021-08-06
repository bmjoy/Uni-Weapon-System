using System;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Core.Movement;
using WeaponSystem.Core.Utils.FireMode;
using WeaponSystem.Core.Weapon.Magazine;

namespace WeaponSystem.Core.Weapon.Action.Control
{
    [AddTypeMenu("Control/Selectable")]
    [Serializable]
    public class SelectableAction : IWeaponAction
    {
        [SerializeReference, SubclassSelector] private IWeaponAction[] _attackActionModes = {new NoneAction()};
        public UnityEvent onSelect;
        private SemiAuto _singleClick;
        private int _index;

        public void Injection(Transform parent, IMagazine magazine)
        {
            foreach (var attackActionMode in _attackActionModes)
            {
                attackActionMode.Injection(parent, magazine);
            }
        }

        public void Action(bool isAction, IPlayerContext context)
        {
            _attackActionModes[_index].Action(isAction, context);
        }

        public void AltAction(bool isAltAction, IPlayerContext context)
        {
            _singleClick ??= new SemiAuto();
            
            if (_singleClick.Evaluate(isAltAction) ==false) return;
            
            _index = ++_index % _attackActionModes.Length;
            onSelect.Invoke();
        }
    }
}