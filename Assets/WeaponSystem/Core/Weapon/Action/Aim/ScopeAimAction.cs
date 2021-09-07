using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Core.Camera;
using WeaponSystem.Core.Movement;
using WeaponSystem.Core.Runtime.FireMode;
using WeaponSystem.Core.Runtime.Timer;
using WeaponSystem.Core.Weapon.Magazine;

namespace WeaponSystem.Core.Weapon.Action.Aim
{
    [Serializable, AddTypeMenu("Aim/ScopeAim")]
    public class ScopeAimAction : IWeaponAction
    {
        [SerializeField] private ScopeCameraBase scopeCamera;
        [SerializeField] private Transform hipPosition;
        [SerializeField] private Transform aimPosition;
        [SerializeField] private List<float> fovScales;
        [SerializeField] private float duration = .2f;
        [SerializeField] private SecondBasedTimer scopedTime;

        public UnityEvent onAimIn;
        public UnityEvent onAimOut;

        private SemiAuto _singleClick = new SemiAuto();
        private Transform _parent;
        private int _fovScaleIndex = 0;

        public void Injection(Transform parent, IMagazine magazine)
        {
            _parent = parent;
            _parent.localPosition = hipPosition.localPosition;
        }

        public void Action(bool isAction, ref bool isAim, IPlayerState state)
        {
            isAim = isAction;

            // move position
            var position = isAction ? aimPosition.localPosition : hipPosition.localPosition;
            _parent.localPosition = Vector3.Slerp(_parent.localPosition, -position, Time.deltaTime / duration);


            scopeCamera.IsActive = scopedTime.IsValid;

            if (scopeCamera.IsActive)
            {
                onAimIn.Invoke();
            }
            else
            {
                onAimOut.Invoke();
            }

            // scoped time
            if (isAction == false)
            {
                scopedTime.Lap();
                return;
            }

            scopeCamera.FieldOfView = ReferenceCameraBase.FieldOfView * fovScales[_fovScaleIndex];
            scopedTime.Update();
        }

        public void AltAction(bool isAltAction, IPlayerState state)
        {
            if (_singleClick.Evaluate(isAltAction) == false) return;
            _fovScaleIndex = ++_fovScaleIndex % fovScales.Count;
        }


        public void OnHolster(ref bool isAim){}


        public void OnDraw(ref bool isAim) {}

    }
}