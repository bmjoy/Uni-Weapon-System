using System;
using UnityEngine;
using WeaponSystem.Camera;
using WeaponSystem.Collision;
using WeaponSystem.Movement;
using WeaponSystem.Runtime;
using WeaponSystem.Weapon.Magazine;
using static UnityEngine.Mathf;

namespace WeaponSystem.Weapon.Action.AltAttackAction
{
    /// <summary>
    /// ズームのみのアクションです。
    /// TPSのエイム動作を実現できます。
    /// </summary>
    [Serializable, AddTypeMenu("Zoom")]
    public class ZoomOnlyAimAction : IAltAttackAction
    {
        [SerializeField, Range(0f, 10f)] private float zoomMultiply = .9f;
        [SerializeField] private float duration = .1f;

        public void Injection(Transform parent, Animator animator, IMagazine magazine) { }

        public void Action(bool isAction, IPlayerContext context)
        {
            context.IsAiming = isAction;
            var toFov = isAction ? FovSettings.BaseFov * zoomMultiply : FovSettings.BaseFov;
            var fromFov = Locator<IReferenceCamera>.Instance.Current.FieldOfView;
            Locator<IReferenceCamera>.Instance.Current.FieldOfView = Lerp(fromFov, toFov, Time.deltaTime / duration);
        }
    }
}