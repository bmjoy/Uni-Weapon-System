using System;
using UnityEngine;
using static UnityEngine.Mathf;
using WeaponSystem.Camera;
using WeaponSystem.Collision;
using WeaponSystem.Movement;
using WeaponSystem.Runtime;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action.AltAttackAction
{
    /// <summary>
    /// FPSでエイムできるようにするアクションです。
    /// Aim時は親のオブジェクトの中心に合わせるように
    /// </summary>
    [Serializable, AddTypeMenu("Aim")]
    public class AimAction : IAltAttackAction
    {
        [SerializeField, Range(Single.Epsilon, 10f)]
        private float duration = .1f;

        [SerializeField] private float zoomMultiply = .7f;
        [SerializeField] private Transform hipPosition;
        [SerializeField] private Transform adsPosition;
        private Transform _transform;
        
        
        public void Injection(Transform parent, Animator animator, IMagazine magazine)
        {
            _transform = parent;
            _transform.localPosition = hipPosition.localPosition;
        }

        public void Action(bool isAction, IPlayerContext context)
        {
            duration = Abs(duration);
            zoomMultiply = Abs(zoomMultiply);
            context.IsAiming = isAction;
            var pos = isAction ? adsPosition.localPosition : hipPosition.localPosition;
            var toFov = isAction ? FovSettings.BaseFov * zoomMultiply : FovSettings.BaseFov;
            var fromFov = Locator<IReferenceCamera>.Instance.Current.FieldOfView;
            Locator<IReferenceCamera>.Instance.Current.FieldOfView = Lerp(fromFov, toFov, Time.deltaTime / duration);
            _transform.localPosition = Vector3.Slerp(_transform.localPosition, -pos, Time.deltaTime / duration);
        }
    }
}