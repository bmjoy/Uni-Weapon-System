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

        [SerializeField] private List<float> zoomMultiplyList = new List<float> {.7f};
        [SerializeField] private Transform hipPosition;
        [SerializeField] private Transform adsPosition;
        [SerializeField] private string aimParamName;
        
        private Animator _animator;
        private Transform _transform;
        private int _aimParamCache;
        private int _index;

        public void Injection(Transform parent, Animator animator, IMagazine magazine)
        {
            _transform = parent;
            _transform.localPosition = hipPosition.localPosition;
            _animator = animator;
            _aimParamCache = Animator.StringToHash(aimParamName);
        }

        public void Action(bool isAction, IPlayerContext context)
        {
            duration = Abs(duration);
            zoomMultiplyList[_index] = Abs(zoomMultiplyList[_index]);
            context.IsAiming = isAction;
            var pos = isAction ? adsPosition.localPosition : hipPosition.localPosition;
            var toFov = isAction ? FovSettings.BaseFov * zoomMultiplyList[_index] : FovSettings.BaseFov;
            var fromFov = Locator<IReferenceCamera>.Instance.Current.FieldOfView;
            Locator<IReferenceCamera>.Instance.Current.FieldOfView = Lerp(fromFov, toFov, Time.deltaTime / duration);
            _transform.localPosition = Vector3.Slerp(_transform.localPosition, -pos, Time.deltaTime / duration);
            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift)) OnIndexChange();
            _animator.SetBool(_aimParamCache, isAction);
        }

        public void OnIndexChange()
        {
            _index = ++_index % zoomMultiplyList.Count;
        }
    }
}