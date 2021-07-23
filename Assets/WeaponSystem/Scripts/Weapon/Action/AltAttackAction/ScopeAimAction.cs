using System;
using UnityEngine;
using WeaponSystem.Scripts.Camera;
using WeaponSystem.Scripts.Movement;
using WeaponSystem.Weapon.Action.Utils;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action.AltAttackAction
{
    [Serializable, AddTypeMenu("ScopeAim")]
    public class ScopeAimAction : IAltAttackAction
    {
        [SerializeField] private ScopeCameraBase scopeCamera;
        [SerializeField] private Transform hipPosition;
        [SerializeField] private Transform aimPosition;
        [SerializeField] private float zoomMultiply = .3f;
        [SerializeField] private float duration = .2f;
        [SerializeField] private SecondBasedTimer scopedTime;

        private Transform _parent;

        public void Injection(Transform parent, Animator animator, IMagazine magazine)
        {
            _parent = parent;
            _parent.localPosition = hipPosition.localPosition;
        }

        public void Action(bool isAction, IPlayerContext context)
        {
            context.IsAiming = isAction;

            // move position
            var position = isAction ? aimPosition.localPosition : hipPosition.localPosition;
            _parent.localPosition = Vector3.Slerp(_parent.localPosition, -position, Time.deltaTime / duration);


            scopeCamera.IsActive = scopedTime.IsValid;
            // scoped time
            if (isAction == false)
            {
                scopedTime.Lap();
                return;
            }

            scopeCamera.FieldOfView = FovSettings.BaseFov * zoomMultiply;
            scopedTime.Update();
        }
    }
}