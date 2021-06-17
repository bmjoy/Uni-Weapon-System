using System;
using UnityEngine;
using WeaponSystem.Runtime;

namespace WeaponSystem.Weapon.Bullet
{
    [Serializable, AddTypeMenu("Projectile")]
    public class ProjectileBullet : IBullet
    {
        [SerializeField] private float bulletSpeed = 500f;
        [SerializeField] private Rigidbody bullet;
        private ObjectPool<Rigidbody> _bulletPool;

        public void Shot(Vector3 position, Vector3 direction)
        {
            _bulletPool ??= new ObjectPool<Rigidbody>(bullet, 10, new GameObject($"{bullet.name} Object Pool").transform);
            var b = _bulletPool.GetObject(position);
            b.gameObject.SetActive(true);
            b.Sleep();
            b.AddForce(direction * bulletSpeed, ForceMode.VelocityChange);
        }
    }
}