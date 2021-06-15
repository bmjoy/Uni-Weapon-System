using System;
using UnityEngine;
using WeaponSystem.Camera;
using WeaponSystem.Movement;
using WeaponSystem.Runtime;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action.AltAttackAction
{
    [Serializable, AddTypeMenu("ZoomOnlyAim")]
    public class ZoomOnlyAimAction : IAltAttackAction
    {
        [SerializeField] private float duration;

        private IPlayerContext _context;
        public void Injection(Transform parent, Animator animator, IMagazine magazine, IPlayerContext context) { }

        public void Action(bool isAction)
        {
            var camera = Locator<IReferenceCamera>.Instance.Current;
            _context.IsAiming = isAction;
        }
    }
}