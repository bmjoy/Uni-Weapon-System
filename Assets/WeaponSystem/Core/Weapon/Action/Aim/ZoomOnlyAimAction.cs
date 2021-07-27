using System;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Core.Camera;
using WeaponSystem.Core.Movement;
using WeaponSystem.Core.Runtime;
using WeaponSystem.Core.Weapon.Magazine;
using static UnityEngine.Mathf;

namespace WeaponSystem.Core.Weapon.Action.Aim
{
    /// <summary>
    /// ズームのみのアクションです。
    /// TPSのエイム動作を実現できます。
    /// </summary>
    [Serializable, AddTypeMenu("Aim/Zoom")]
    public class ZoomOnlyAimAction : IWeaponAction
    {
        [SerializeField, Range(0f, 10f)] private float[] zoomMultiplyList = {.9f};
        [SerializeField] private float duration = .1f;
        public UnityEvent onZoomIn;
        public UnityEvent onZoomOut;
        public void Injection(Transform parent, IMagazine magazine) { }

        public void Action(bool isAction, IPlayerContext context)
        {
            context.IsAiming = isAction;
            var toFov = isAction ? ReferenceCameraBase.FieldOfView * zoomMultiplyList[0] : ReferenceCameraBase.FieldOfView;
            var fromFov = Locator<ReferenceCameraBase>.Instance.Current.FovMultiple;
            Locator<ReferenceCameraBase>.Instance.Current.FovMultiple = Lerp(fromFov, toFov, Time.deltaTime / duration);

            if (isAction)
                onZoomIn.Invoke();
            else
                onZoomOut.Invoke();
        }

        public void AltAction(bool isAltAction, IPlayerContext context) { }
    }
}