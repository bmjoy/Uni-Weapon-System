using System;
using UnityEngine;
using WeaponSystem.Collision;

namespace WeaponSystem.Weapon.Bullet
{
    public class RocketAmmo : ProjectileAmmo
    {
        public override IObjectPermission ObjectPermission { get; set; }
        public override IObjectGroup ObjectGroup { get; set; }
        public override Rigidbody Rigidbody => _rigidbody;

        private Rigidbody _rigidbody;

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        private void Update()
        {
            
        }
    }
}