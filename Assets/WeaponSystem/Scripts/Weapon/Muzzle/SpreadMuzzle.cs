using System;
using UnityEngine;
using WeaponSystem.Camera;
using WeaponSystem.Runtime;
using WeaponSystem.Scripts.Movement;
using static UnityEngine.Quaternion;

namespace WeaponSystem.Weapon.Muzzle
{
    [Serializable, AddTypeMenu("Spread")]
    public class SpreadMuzzle : IMuzzle
    {
        [SerializeField] private Transform reference;
        [SerializeField] private SpreadSetting spreadSetting;
        public Vector3 Position => reference.position;
        public Vector3 Direction => reference.forward;
        public Quaternion Rotation => reference.rotation;

        public void Defuse(IPlayerContext context)
        {
            var camera = Locator<IReferenceCamera>.Instance.Current.Center;
            var spread = spreadSetting[context.State];
            var defuse = camera.rotation * spread.Defuse(context.IsAiming) + camera.forward * spread.Distance;
            reference.rotation = LookRotation(defuse);
        }
    }
}