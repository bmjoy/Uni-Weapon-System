using System;
using UnityEngine;
using WeaponSystem.Collision;
using WeaponSystem.Movement;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action.AttackAction
{
    [Serializable, AddTypeMenu("None")]
    public class NoneAttackAction : IAttackAction
    {
        public void Action(bool isAction, IPlayerContext context) { }
        public void Injection(Transform parent, Animator animator, IMagazine magazine) { }
    }
}