using System;
using UnityEngine;
using WeaponSystem.Movement;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action.AttackAction
{
    [Serializable, AddTypeMenu("None")]
    public class NoneAttackAction : IAttackAction
    {
        public void Injection(Transform parent, Animator animator, IMagazine magazine) { }
        public void Action(bool isAction, IPlayerContext context) { }
    }
}