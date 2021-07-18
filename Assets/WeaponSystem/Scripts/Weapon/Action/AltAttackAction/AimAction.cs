using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;
using WeaponSystem.Camera;
using WeaponSystem.Movement;
using WeaponSystem.Runtime;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action.AltAttackAction
{
    /// <summary>
    /// FPSでエイムできるようにするアクションです。
    /// </summary>
    [Serializable, AddTypeMenu("Aim")]
    public class AimAction : IAltAttackAction
    {
        [SerializeField, Range(Single.Epsilon, 10f)]
        private float duration = .1f;

        [SerializeField] private List<Sight> sights;
        [SerializeField] private Transform hipPosition;
        [SerializeField] private int scopeIndex;
        [SerializeField] private string aimParamName = "Aiming";

        private Animator _animator;
        private Transform _transform;
        private int? _aimParamCache;

        public void Injection(Transform parent, Animator animator, IMagazine magazine)
        {
            _transform = parent;
            _transform.localPosition = hipPosition.localPosition;
            _animator = animator;
        }

        public void Action(bool isAction, IPlayerContext context)
        {
            duration = Abs(duration);
            context.IsAiming = isAction;


            var currentSight = sights[scopeIndex];

            scopeIndex = scopeIndex % sights.Count;
            for (int i = 0; i < sights.Count; i++)
            {
                sights[i].gameObject.SetActive(i == scopeIndex);
            }

            var pos = isAction ? sights[scopeIndex].AimPoint.localPosition : hipPosition.localPosition;
            var toFov = isAction ? FovSettings.BaseFov * sights[scopeIndex].ZoomMultiple : FovSettings.BaseFov;
            var fromFov = Locator<IReferenceCamera>.Instance.Current.FieldOfView;
            Locator<IReferenceCamera>.Instance.Current.FieldOfView =
                Lerp(fromFov, toFov, Time.deltaTime / currentSight.Duration);
            _transform.localPosition =
                Vector3.Slerp(_transform.localPosition, -pos, Time.deltaTime / currentSight.Duration);

            if (aimParamName == String.Empty) return;
            _aimParamCache ??= Animator.StringToHash(aimParamName);
            _animator.SetBool(_aimParamCache!.Value, isAction);
        }

        public void OnScopeChange(int index)
        {
            scopeIndex = index % sights.Count;
            for (int i = 0; i < sights.Count; i++)
            {
                sights[i].gameObject.SetActive(i == scopeIndex);
            }
        }
    }
}