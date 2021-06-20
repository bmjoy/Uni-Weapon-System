using System;
using UnityEngine;
using WeaponSystem.Collision;
using WeaponSystem.Runtime;

namespace WeaponSystem.Weapon.Bullet
{
    [Serializable, AddTypeMenu("Projectile")]
    public class ProjectileBullet : IBullet
    {
        [SerializeField] private float bulletSpeed = 500f;
        [SerializeField] private ProjectileAmmo ammo;
        private ObjectPool<ProjectileAmmo> _ammoPool;

        public void Shot(Vector3 position, Vector3 direction, IObjectPermission permission, IObjectGroup group)
        {
            _ammoPool ??= new ObjectPool<ProjectileAmmo>(ammo, 10,
                new GameObject($"{ammo.name} Object Pool").transform);

            var fireAmmo = _ammoPool.GetObject(position);
            fireAmmo.gameObject.SetActive(true);
            fireAmmo.Rigidbody.Sleep();
            fireAmmo.ObjectGroup = group;
            fireAmmo.ObjectPermission = permission;
            fireAmmo.Rigidbody.AddForce(direction * bulletSpeed, ForceMode.VelocityChange);
        }
    }
}