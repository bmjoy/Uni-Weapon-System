using System;
using UnityEngine;
using WeaponSystem.Camera;
using WeaponSystem.Movement;
using WeaponSystem.Runtime;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action.AltAttackAction
{
    [Serializable, AddTypeMenu("Zoom")]
    public class ZoomOnlyAimAction : IAltAttackAction
    {
        [SerializeField] private float duration;
        public void Injection(Transform parent, Animator animator, IMagazine magazine) { }

        public void Action(bool isAction, IPlayerContext context)
        {
            context.IsAiming = isAction;
            
        }
    }
}