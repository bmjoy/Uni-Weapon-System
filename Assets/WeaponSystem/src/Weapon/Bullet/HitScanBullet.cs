using System;
using UnityEngine;
using WeaponSystem.Collision;
using WeaponSystem.Effect;
using WeaponSystem.Runtime;
using static UnityEngine.Physics;

namespace WeaponSystem.Weapon.Bullet
{
    [Serializable, AddTypeMenu("HitScan")]
    public class HitScanBullet : IBullet
    {
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private float hitRadius = .025f;
        [SerializeField] private float bulletImpactPower = 10f;
        [SerializeField] private Traser tracer;
        [SerializeField] private LayerMask collisionLayer = AllLayers;
        [SerializeReference, SubclassSelector] private IEffect _hitEffect = new NoneEffect();
        private ObjectPool<Traser> _tracerPool;

        public void Shot(Vector3 position, Vector3 direction, IObjectPermission permission, IObjectGroup group)
        {
            _tracerPool ??= new ObjectPool<Traser>(tracer, 10, new GameObject($"{tracer.name} Object Pool").transform);
            var ray = new Ray(position, direction);

            var t = _tracerPool.GetObject();
            t.StartPoint = position;
            t.EndPoint = direction * bulletConfig.MaxDistance + position;

            if (SphereCast(ray, hitRadius, out RaycastHit hit, bulletConfig.MaxDistance, collisionLayer) ==
                false) return;

            t.EndPoint = hit.point;

            if (hit.collider.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddForce(-hit.normal * bulletImpactPower, ForceMode.Impulse);
            }

            _hitEffect?.Play(hit.point, Quaternion.LookRotation(hit.normal), null);

            if (group == null || permission == null) return;

            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                if (damageable == null) return;

                if (damageable.ObjectGroup.SelfId == group.SelfId && permission.SelfDamage)
                {
                    damageable.AddDamage(bulletConfig.GetDamage(damageable.HitType, hit.distance));
                }

                if (damageable.ObjectGroup.GroupId == group.GroupId && permission.TeamDamage)
                {
                    damageable.AddDamage(bulletConfig.GetDamage(damageable.HitType, hit.distance));
                }

                if (damageable.ObjectGroup.GroupId != group.GroupId && permission.EnemyDamage)
                {
                    damageable.AddDamage(bulletConfig.GetDamage(damageable.HitType, hit.distance));
                }
            }

            _tracerPool.Clear();
        }
    }
}