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
        [SerializeField, Range(0f, 10f)] private float[] zoomScaleList = {.9f};
        [SerializeField] private float duration = .1f;
        public UnityEvent onZoomIn;
        public UnityEvent onZoomOut;

        public void Injection(Transform parent, IMagazine magazine) { }


        public void Action(bool isAction, ref bool isAim, IPlayerState state)
        {
            isAim = isAction;
            var toFov = isAction ? zoomScaleList[0] : 1f;
            var fromFov = Locator<ReferenceCameraBase>.Instance.Current.FovScale;
            Locator<ReferenceCameraBase>.Instance.Current.FovScale = Lerp(fromFov, toFov, Time.deltaTime / duration);

            if (isAction) onZoomIn.Invoke();
            else onZoomOut.Invoke();
        }


        public void AltAction(bool isAltAction, IPlayerState state) { }

        public void OnHolster(ref bool isAim) { }


        public void OnDraw(ref bool isAim) { }
    }
}