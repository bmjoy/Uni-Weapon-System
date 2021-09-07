using System;
using UnityEngine;
using WeaponSystem.Core.Camera;
using WeaponSystem.Core.Movement;
using WeaponSystem.Core.Runtime;
using static UnityEngine.Quaternion;


namespace WeaponSystem.Core.Weapon.Muzzle
{
    [Serializable, AddTypeMenu("Spread")]
    public class SpreadMuzzle : IMuzzle
    {
        [SerializeField] private Transform reference;
        [SerializeField] private SpreadProfile spreadProfile;


        public Vector3 Position => reference.position;
        public Vector3 Direction => reference.forward;
        public Quaternion Rotation => reference.rotation;


        public void Defuse(IPlayerState state, bool isAim)
        {
            var camera = Locator<ReferenceCameraBase>.Instance.Current.Center;
            var spread = spreadProfile[state.State];
            var defuse = camera.rotation * spread.Defuse(isAim) + camera.forward;
            reference.rotation = LookRotation(defuse);
        }
    }
}