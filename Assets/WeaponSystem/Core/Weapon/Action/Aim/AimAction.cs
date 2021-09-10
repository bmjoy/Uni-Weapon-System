using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Core.Camera;
using WeaponSystem.Core.Movement;
using WeaponSystem.Core.Runtime;
using WeaponSystem.Core.Runtime.FireMode;
using WeaponSystem.Core.Weapon.Magazine;
using static UnityEngine.Mathf;


namespace WeaponSystem.Core.Weapon.Action.Aim
{
    /// <summary>
    /// FPS視点でエイムできるようにするアクションです。
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
        public UnityEvent onZoomChange;
        public UnityEvent onSightChange;

        private IFireMode _singleClick = new SemiAuto();
        private Transform _self;

        private bool _isAim;


        public void Injection(Transform parent, IMagazine magazine)
        {
            _self = parent;
            _self.localPosition = -hipPosition.localPosition;

            for (int i = 0; i < sights.Count; i++) { sights[i].gameObject.SetActive(i == sightIndex); }
        }


        public void Action(bool isAction, ref bool isAim, IPlayerState state)
        {
            duration = Abs(duration);
            
            _isAim = isAction;
            isAim = _isAim;

            if (_isAim) { onAimIn.Invoke(); }
            else { onAimOut.Invoke(); }

            var currentSight = sights[sightIndex];

            var referenceCamera = Locator<ReferenceCameraBase>.Instance.Current;

            var position = _isAim ? sights[sightIndex].AimPoint.localPosition : hipPosition.localPosition;

            var to = _isAim ? sights[sightIndex].ZoomMultiples : 1f;

            var from = referenceCamera.FovScale;

            referenceCamera.FovScale = Lerp(from, to, Time.deltaTime / currentSight.Duration);

            _self.localPosition = Vector3.Slerp(_self.localPosition, -position, Time.deltaTime / currentSight.Duration);
            sightIndex = sightIndex % sights.Count;
        }


        public void AltAction(bool isAltAction, IPlayerState state)
        {
            if (_isAim == false) return;
            if (_singleClick.Evaluate(isAltAction) == false) return;

            sights[sightIndex].FovScaleChange();
            onZoomChange.Invoke();
        }


        public void OnHolster(ref bool isAim)
        {
            _isAim = false;
            isAim = _isAim;
            _self.localPosition = -hipPosition.localPosition;
        }


        public void OnDraw(ref bool isAim)
        {
            _isAim = false;
            isAim = _isAim;
            _self.localPosition = -hipPosition.localPosition;
        }


        public void SightChange(int index)
        {
            sightIndex = index % sights.Count;
            onSightChange.Invoke();
            for (int i = 0; i < sights.Count; i++) sights[i].gameObject.SetActive(i == sightIndex);
        }
    }
}