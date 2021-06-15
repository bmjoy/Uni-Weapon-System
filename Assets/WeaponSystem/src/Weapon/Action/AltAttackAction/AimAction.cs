using System;
using UnityEngine;
using WeaponSystem.Camera;
using WeaponSystem.Movement;
using WeaponSystem.Runtime;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action.AltAttackAction
{
    [Serializable, AddTypeMenu("Aim Action")]
    public class AimAction : IAltAttackAction
    {
        [SerializeField, Range(Single.Epsilon, 10f)]
        private float duration = .1f;

        [SerializeField] private float zoom;
        [SerializeField] private Transform hipPosition;
        [SerializeField] private Transform adsPosition;

        private Transform _transform;
        private IPlayerContext _context;

        public void Injection(Transform parent, Animator animator, IMagazine magazine, IPlayerContext context)
        {
            _transform = parent;
            _transform.localPosition = hipPosition.localPosition;
            _context = context;
        }

        public void Action(bool isAction)
        {
            Locator<IPlayerContext>.Instance.Current.IsAiming = isAction;

            var pos = isAction ? adsPosition.localPosition : hipPosition.localPosition;
            var toFov = isAction ? FovSettings.BaseFov * zoom : FovSettings.BaseFov;
            var fromFov = Locator<IReferenceCamera>.Instance.Current.FieldOfView;
            Locator<IReferenceCamera>.Instance.Current.FieldOfView =
                Mathf.Lerp(fromFov, toFov, Time.deltaTime / duration);
            _transform.localPosition = Vector3.Slerp(_transform.localPosition, -pos, Time.deltaTime / duration);
        }
    }
}