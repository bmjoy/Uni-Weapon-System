﻿using System;
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
        [SerializeField] private SpreadSetting spreadSetting;
        public Vector3 Position => reference.position;
        public Vector3 Direction => reference.forward;
        public Quaternion Rotation => reference.rotation;

        public void Defuse(IPlayerContext context)
        {
            var camera = Locator<ReferenceCameraBase>.Instance.Current.Center;
            var spread = spreadSetting[context.State];
            var defuse = camera.rotation * spread.Defuse(context.IsAiming) + camera.forward * spread.Distance;
            reference.rotation = LookRotation(defuse);
        }
    }
}