using System;
using UnityEngine;
using WeaponSystem.Collision;
using WeaponSystem.Scripts.Movement;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action.AltAttackAction
{
    [Serializable, AddTypeMenu("None")]
    public class NoneAltAttackAction : IAltAttackAction
    {
        // 何もしないよ!
        public void Action(bool isAction, IPlayerContext context) { }
        public void Injection(Transform parent, Animator animator, IMagazine magazine) { }
    }
}