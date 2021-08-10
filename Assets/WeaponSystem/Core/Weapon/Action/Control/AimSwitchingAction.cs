using System;
using UnityEngine;
using WeaponSystem.Core.Movement;
using WeaponSystem.Core.Weapon.Magazine;

namespace WeaponSystem.Core.Weapon.Action.Control
{
    /// <summary>
    /// Aim時に挙動を変えることができるアクション
    /// Valorantのヴァンダルのような動作を実現するためのクラス
    /// </summary>
    [Serializable, AddTypeMenu("Control/AimSwitching")]
    public class AimSwitchingAction : IWeaponAction
    {
        [SerializeReference, SubclassSelector] private IWeaponAction _attackAction = new NoneAction();
        [SerializeReference, SubclassSelector] private IWeaponAction _aimingAttackAction = new NoneAction();

        public void Injection(Transform parent, IMagazine magazine)
        {
            _attackAction.Injection(parent, magazine);
            _aimingAttackAction.Injection(parent, magazine);
        }

        public void Action(bool isAction, ref bool isAim, IPlayerState state)
        {
            if (isAim)
            {
                _aimingAttackAction.Action(isAction, ref isAim, state);
                return;
            }

            _attackAction.Action(isAction, ref isAim, state);
        }

        public void AltAction(bool isAltAction, IPlayerState state) { }
    }
}