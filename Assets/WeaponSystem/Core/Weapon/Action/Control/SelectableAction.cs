using System;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Core.Debug;
using WeaponSystem.Core.Movement;
using WeaponSystem.Core.Runtime.FireMode;
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

        public void Action(bool isAction, ref bool isAim, IPlayerState state)
        {
            _attackActionModes[_index].Action(isAction, ref isAim, state);
        }

        public void AltAction(bool isAltAction, IPlayerState state)
        {
            _singleClick ??= new SemiAuto();

            if (_singleClick.Evaluate(isAltAction) == false) return;
            "select".Log();
            _index = ++_index % _attackActionModes.Length;
            onSelect.Invoke();
        }


        public void OnHolster(ref bool isAim) {}


        public void OnDraw(ref bool isAim) {}

    }
}