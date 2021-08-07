using System;
using UnityEngine;
using WeaponSystem.Core.Collision;
using WeaponSystem.Core.ObjectPool;
using WeaponSystem.Core.Runtime;
using WeaponSystem.Core.Weapon.Bullet.Ammo;

namespace WeaponSystem.Core.Weapon.Bullet
{
    [Serializable, AddTypeMenu("Projectile")]
    public class ProjectileBullet : IBullet
    {
        [SerializeField] private float bulletSpeed = 500f;
        [SerializeField] private ProjectileAmmo ammo;
        private IObjectPool<ProjectileAmmo> _ammoPool;
        
        public void Shot(Vector3 position, Vector3 direction, IObjectPermission permission, IObjectGroup group)
        {
            _ammoPool ??= Locator<IObjectPoolFactory>.Instance.Current.CreatePool(ammo, 10);

            var fireAmmo = _ammoPool.GetObject();
            fireAmmo.transform.position = position;
            fireAmmo.gameObject.SetActive(true);
            fireAmmo.ObjectGroup = group;
            fireAmmo.ObjectPermission = permission;
            fireAmmo.AddForce(direction * bulletSpeed);
        }
    }
}