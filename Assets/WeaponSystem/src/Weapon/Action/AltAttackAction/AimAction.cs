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
    /// Aim時は親のオブジェクトの中心に合わせるように
    /// </summary>
    [Serializable, AddTypeMenu("Aim")]
    public class AimAction : IAltAttackAction
    {
        [SerializeField, Range(Single.Epsilon, 10f)]
        private float duration = .1f;

        [SerializeField] private List<float> zoomMultiplyList = new List<float> {.7f};
        [SerializeField] private Transform hipPosition;
        [SerializeField] private Transform adsPosition;
        private Transform _transform;
        private int _index;

        public void Injection(Transform parent, Animator animator, IMagazine magazine)
        {
            _transform = parent;
            _transform.localPosition = hipPosition.localPosition;
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
        }

        public void OnIndexChange() => _index = (++_index) % zoomMultiplyList.Count;
    }
}