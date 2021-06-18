using System;
using UnityEngine;
using WeaponSystem.Camera;
using WeaponSystem.Movement;
using WeaponSystem.Runtime;

namespace WeaponSystem.Weapon.Muzzle
{
    [Serializable, AddTypeMenu("ScreenSpace")]
    public class ScreenSpaceMuzzle : IMuzzle
    {
        [SerializeField] private float distance = 10f;
        private Vector3 _offset;

        public Vector3 Position => Locator<IReferenceCamera>.Instance.Current.Camera.transform.position;

        public Vector3 Direction => Locator<IReferenceCamera>.Instance.Current.Camera.transform.forward;
        public Quaternion Rotation => Locator<IReferenceCamera>.Instance.Current.Camera.transform.rotation;

        public void Defuse(IPlayerContext context)
        {
            if (context.IsAiming)
            {
                _offset = Vector3.zero;
            }
        }
    }
}