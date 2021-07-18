using System;
using ObjectPool;
using UnityEngine;
using WeaponSystem.Collision;

namespace WeaponSystem.Weapon.Bullet
{
    [Serializable, AddTypeMenu("Projectile")]
    public class ProjectileBullet : IBullet
    {
        [SerializeField] private float bulletSpeed = 500f;
        [SerializeField] private ProjectileAmmo ammo;
        private IObjectPool<ProjectileAmmo> _ammoPool;

        public void Shot(Vector3 position, Vector3 direction, IObjectPermission permission, IObjectGroup group)
        {
            _ammoPool ??= new ObjectPool<ProjectileAmmo>(ammo, 10);

            var fireAmmo = _ammoPool.GetObject(position, Quaternion.identity);
            fireAmmo.gameObject.SetActive(true);
            fireAmmo.ObjectGroup = group;
            fireAmmo.ObjectPermission = permission;
            fireAmmo.AddForce(direction * bulletSpeed);
        }
    }
}