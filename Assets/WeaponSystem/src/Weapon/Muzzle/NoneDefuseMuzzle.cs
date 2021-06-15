﻿using System;
using UnityEngine;
using WeaponSystem.Camera;
using WeaponSystem.Runtime;

namespace WeaponSystem.Weapon.Muzzle
{
    [Serializable, AddTypeMenu("NoneDefuzeMuzzle")]
    public class NoneDefuseMuzzle : IMuzzle
    {
        [SerializeField] private Transform reference;
        [SerializeField] private float accuracyDistance = 1000f;
        public Vector3 Position => reference.position;
        public Vector3 Direction => reference.forward;
        public Quaternion Rotation => reference.rotation;

        public void Defuse()
        {
            if (Locator<IReferenceCamera>.Instance.IsValid == false) return;
            var forward = Locator<IReferenceCamera>.Instance.Current.Center.forward;
            var look = Quaternion.LookRotation(forward * accuracyDistance);
            reference.rotation = look;
        }
    }
}