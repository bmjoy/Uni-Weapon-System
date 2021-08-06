using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Core.Camera;
using WeaponSystem.Core.Debug;
using WeaponSystem.Core.Movement;
using WeaponSystem.Core.Runtime;
using WeaponSystem.Core.Utils.FireMode;
using WeaponSystem.Core.Weapon.Magazine;
using static UnityEngine.Mathf;

namespace WeaponSystem.Core.Weapon.Action.Aim
{
    /// <summary>
    /// FPSでエイムできるようにするアクションです。
    /// </summary>
    [Serializable, AddTypeMenu("Aim/Aim")]
    public class AimAction : IWeaponAction
    {
        [SerializeField, Range(Single.Epsilon, 10f)]
        private float duration = .1f;

        [SerializeField] private List<Sight> sights;
        [SerializeField] private Transform hipPosition;
        [SerializeField] private int sightIndex;

        public UnityEvent onAimIn;
        public UnityEvent onAimOut;
        public UnityEvent onSightChange;

        private IFireMode _singleClick = new SemiAuto();
        private Transform _self;

        private bool _isAim;

        public void Injection(Transform parent, IMagazine magazine)
        {
            _self = parent;
            _self.localPosition = -hipPosition.localPosition;
            for (int i = 0; i < sights.Count; i++)
            {
                sights[i].gameObject.SetActive(i == sightIndex);
            }
        }

        public void Action(bool isAction, IPlayerContext context)
        {
            duration = Abs(duration);
            context.IsAiming = isAction;
            _isAim = isAction;
            if (isAction)
            {
                onAimIn.Invoke();
            }
            else
            {
                onAimOut.Invoke();
            }

            var currentSight = sights[sightIndex];

            var referenceCamera = Locator<ReferenceCameraBase>.Instance.Current;

            var position = isAction ? sights[sightIndex].AimPoint.localPosition : hipPosition.localPosition;

            var to = isAction ? sights[sightIndex].ZoomMultiples : 1;

            var from = referenceCamera.FovMultiple;

            referenceCamera.FovMultiple = Lerp(from, to, Time.deltaTime / currentSight.Duration);

            _self.localPosition = Vector3.Slerp(_self.localPosition, -position, Time.deltaTime / currentSight.Duration);
            sightIndex = sightIndex % sights.Count;
        }

        public void AltAction(bool isAltAction, IPlayerContext context)
        {
            if (_isAim == false) return;
            if (_singleClick.Evaluate(isAltAction) == false) return;
            sights[sightIndex].ZoomChange();
            onSightChange.Invoke();
        }

        public void SightChange(int index)
        {
            sightIndex = index % sights.Count;

            for (int i = 0; i < sights.Count; i++) sights[i].gameObject.SetActive(i == sightIndex);
        }
    }
}