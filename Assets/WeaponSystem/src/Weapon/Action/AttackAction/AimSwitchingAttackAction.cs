using System;
using UnityEngine;
using WeaponSystem.Collision;
using WeaponSystem.Movement;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action.AttackAction
{
    /// <summary>
    /// Aim時に挙動を変えることができるアクション
    /// Valorantのヴァンダルのような動作を実現するためのクラス
    /// </summary>
    [Serializable, AddTypeMenu("Control/AimSwitching")]
    public class AimSwitchingAttackAction : IAttackAction
    {
        [SerializeReference, SubclassSelector] private IAttackAction _attackAction = new NoneAttackAction();
        [SerializeReference, SubclassSelector] private IAttackAction _aimingAttackAction = new NoneAttackAction();

        public void Injection(Transform parent, Animator animator, IMagazine magazine)
        {
            _attackAction.Injection(parent, animator, magazine);
            _attackAction.Injection(parent, animator, magazine);
        }

        public void Action(bool isAction, IPlayerContext context)
        {
            if (context?.IsAiming ?? false) _aimingAttackAction.Action(isAction, context);
            _attackAction.Action(isAction, context);
        }
    }
}