using System;
using UnityEngine;
using WeaponSystem.Core.Camera;
using WeaponSystem.Core.Movement;
using WeaponSystem.Core.Runtime;
using Random = UnityEngine.Random;

namespace WeaponSystem.Core.Weapon.Muzzle
{
    [Serializable, AddTypeMenu("ScreenSpace")]
    public class ScreenSpaceMuzzle : IMuzzle
    {
        [SerializeField] private float distance = 10f;
        private Vector3 _offset;

        public Vector3 Position
        {
            get
            {
                var referenceCamera = Locator<ReferenceCamera>.Instance.Current;

                return referenceCamera.Center.position +
                       referenceCamera.Center.forward * referenceCamera.Camera.nearClipPlane;
            }
        }

        public Vector3 Direction => Locator<ReferenceCamera>.Instance.Current.Camera.transform.forward + _offset;
        public Quaternion Rotation => Locator<ReferenceCamera>.Instance.Current.Camera.transform.rotation;

        public void Defuse(IPlayerContext context)
        {
            if (context.IsAiming)
            {
                _offset = Vector3.zero;
            }

            _offset = Rotation * Random.insideUnitCircle;
            _offset /= distance;
        }
    }
}