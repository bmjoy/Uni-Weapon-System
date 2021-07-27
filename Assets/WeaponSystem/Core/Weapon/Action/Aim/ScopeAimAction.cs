using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Core.Camera;
using WeaponSystem.Core.Movement;
using WeaponSystem.Core.Utils.FireMode;
using WeaponSystem.Core.Utils.Timer;
using WeaponSystem.Core.Weapon.Magazine;

namespace WeaponSystem.Core.Weapon.Action.Aim
{
    [Serializable, AddTypeMenu("Aim/ScopeAim")]
    public class ScopeAimAction : IWeaponAction
    {
        [SerializeField] private ScopeCameraBase scopeCamera;
        [SerializeField] private Transform hipPosition;
        [SerializeField] private Transform aimPosition;
        [SerializeField] private List<float> zoomMultiplyList;
        [SerializeField] private float duration = .2f;
        [SerializeField] private SecondBasedTimer scopedTime;

        public UnityEvent onAimIn;
        public UnityEvent onAimOut;

        private SemiAuto _singleClick = new SemiAuto();
        private Transform _parent;
        private int _zoomMultiplyIndex = 0;
        
        public void Injection(Transform parent, IMagazine magazine)
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

            scopeCamera.FieldOfView = ReferenceCameraBase.FieldOfView * zoomMultiplyList[_zoomMultiplyIndex];
            scopedTime.Update();
        }

        public void AltAction(bool isAltAction, IPlayerContext context)
        {
            if (_singleClick.Evaluate(isAltAction) == false) return;
            _zoomMultiplyIndex = ++_zoomMultiplyIndex % zoomMultiplyList.Count;
        }
    }
}