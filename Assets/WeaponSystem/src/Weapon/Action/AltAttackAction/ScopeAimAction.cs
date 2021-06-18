using System;
using UnityEngine;
using WeaponSystem.Collision;
using WeaponSystem.Movement;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action.AltAttackAction
{
    [Serializable]
    public class ScopeAimAction : IAltAttackAction
    {
        [SerializeField] private float duration;

        public void Injection(Transform parent, Animator animator, IMagazine magazine) { }

        public void Action(bool isAction, IPlayerContext context)
        {
            context.IsAiming = isAction;
        }
    }
}