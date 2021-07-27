using System;
using EffectSystem;
using ObjectPool;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Core.Collision;
using static UnityEngine.Physics;

namespace WeaponSystem.Core.Weapon.Bullet
{
    [Serializable, AddTypeMenu("HitScan")]
    public class HitScanBullet : IBullet
    {
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private float hitRadius = .025f;
        [SerializeField] private float bulletImpactPower = 10f;
        [SerializeField] private Tracer tracer;
        [SerializeField] private LayerMask collisionLayer = AllLayers;
        private IObjectPool<Tracer> _tracerPool;

        public UnityEvent onHit;
        public UnityEvent<RaycastHit> onSelfHit;
        public UnityEvent<RaycastHit> onFriendlyHit;
        public UnityEvent<RaycastHit> onEnemyHit;

        public void Shot(Vector3 position, Vector3 direction, IObjectPermission permission, IObjectGroup group)
        {
            _tracerPool ??= new ObjectPool<Tracer>(tracer, 15);
            var ray = new Ray(position, direction);

            var current = _tracerPool.GetObject();
            current.gameObject.SetActive(true);
            current.StartPoint = position;

            if (SphereCast(ray, hitRadius, out RaycastHit hit, bulletConfig.MaxDistance, collisionLayer) == false)
            {
                current.EndPoint = direction * bulletConfig.MaxDistance + position;
                return;
            }

            onHit.Invoke();
            current.EndPoint = hit.point;

            if (hit.collider.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddForce(-hit.normal * bulletImpactPower, ForceMode.Impulse);
            }

            if (group == null || permission == null) return;

            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                if (damageable.ObjectGroup.SelfId == group.SelfId && permission.SelfDamage)
                {
                    onSelfHit.Invoke(hit);
                    damageable.AddDamage(bulletConfig.GetDamage(damageable.HitType, hit.distance));
                }

                if (damageable.ObjectGroup.GroupId == group.GroupId && permission.TeamDamage)
                {
                    onSelfHit.Invoke(hit);
                    damageable.AddDamage(bulletConfig.GetDamage(damageable.HitType, hit.distance));
                }

                if (damageable.ObjectGroup.GroupId != group.GroupId && permission.EnemyDamage)
                {
                    onSelfHit.Invoke(hit);
                    damageable.AddDamage(bulletConfig.GetDamage(damageable.HitType, hit.distance));
                }
            }
        }
    }
}