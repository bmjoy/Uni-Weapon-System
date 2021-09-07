using System;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Core.Collision;
using WeaponSystem.Core.Collision.ObjectMaterial;
using WeaponSystem.Core.ObjectPool;
using WeaponSystem.Core.Runtime;
using WeaponSystem.Core.Weapon.Bullet.Ammo;
using static UnityEngine.Physics;

namespace WeaponSystem.Core.Weapon.Bullet
{
    [Serializable, AddTypeMenu("HitScan")]
    public class HitScanBullet : IBullet
    {
        [SerializeField] private BulletDamageProfile bulletDamageProfile;
        [SerializeField] private float hitRadius = .025f;
        [SerializeField] private float bulletImpactPower = 10f;
        [SerializeField] private LayerMask collisionLayer = AllLayers;
        [SerializeField] private Tracer tracer;

        public UnityEvent<ObjectInfo> onHit;
        public UnityEvent<RaycastHit> onSelfHit;
        public UnityEvent<RaycastHit> onFriendlyHit;
        public UnityEvent<RaycastHit> onEnemyHit;

        private IObjectPool<Tracer> _tracerPool;

        public void Shot(Vector3 position, Vector3 direction, IObjectPermission permission, IObjectGroup group)
        {
            var ray = new Ray(position, direction);

            if (SphereCast(ray, hitRadius, out RaycastHit hit, bulletDamageProfile.MaxDistance, collisionLayer) == false)
            {
                if (tracer != null)
                {
                    _tracerPool ??= Locator<IObjectPoolFactory>.Instance.Current.CreatePool(tracer, 10);
                    _tracerPool.GetObject().SetPosition(position, direction * 10000f);
                }

                return;
            }

            if (tracer != null)
            {
                _tracerPool ??= Locator<IObjectPoolFactory>.Instance.Current.CreatePool(tracer, 10);
                _tracerPool.GetObject().SetPosition(position, hit.point);
            }

            var info = hit.collider.TryGetComponent(out IObjectMaterial material)
                ? new ObjectInfo(material, hit.transform, hit.normal)
                : new ObjectInfo(null, hit.transform, hit.normal);

            onHit.Invoke(info);

            var distance = hit.distance;

            if (hit.collider.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddForce(-hit.normal * (bulletImpactPower * bulletDamageProfile.GetImpact(distance)),
                    ForceMode.Impulse);
            }

            if (group == null || permission == null) return;

            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                if (damageable.ObjectGroup.SelfId == group.SelfId)
                {
                    onSelfHit.Invoke(hit);

                    if (permission.SelfDamage)
                    {
                        damageable.AddDamage(bulletDamageProfile.GetDamage(damageable.BodyType, distance));
                    }
                }

                if (damageable.ObjectGroup.GroupId == group.GroupId)
                {
                    onFriendlyHit.Invoke(hit);

                    if (permission.TeamDamage)
                    {
                        damageable.AddDamage(bulletDamageProfile.GetDamage(damageable.BodyType, distance));
                    }
                }

                if (damageable.ObjectGroup.GroupId != group.GroupId)
                {
                    onEnemyHit.Invoke(hit);

                    if (permission.EnemyDamage)
                    {
                        damageable.AddDamage(bulletDamageProfile.GetDamage(damageable.BodyType, distance));
                    }
                }
            }
        }
    }
}