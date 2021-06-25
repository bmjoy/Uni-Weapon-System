using UnityEngine;
using WeaponSystem.Collision;

namespace WeaponSystem.Weapon.Bullet
{
    [RequireComponent(typeof(ConstantForce), typeof(Collider))]
    public class RocketAmmo : ProjectileAmmo
    {
        public override IObjectPermission ObjectPermission { get; set; }
        public override IObjectGroup ObjectGroup { get; set; }
        public override void AddForce(Vector3 force) => _force.force = force;

        private ConstantForce _force;

        private void Awake() => _force = GetComponent<ConstantForce>();

        private void OnCollisionEnter(UnityEngine.Collision other) { }
    }
}