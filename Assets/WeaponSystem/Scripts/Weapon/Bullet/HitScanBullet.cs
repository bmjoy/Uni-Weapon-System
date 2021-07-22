using System;
using AudioSystem;
using AudioSystem.ObjectMaterial;
using ObjectPool;
using UnityEngine;
using WeaponSystem.Collision;
using WeaponSystem.Effect;
using WeaponSystem.Scripts.Debug;
using WeaponSystem.Scripts.Runtime;
using static UnityEngine.Physics;

namespace WeaponSystem.Weapon.Bullet
{
    [Serializable, AddTypeMenu("HitScan")]
    public class HitScanBullet : IBullet
    {
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private float hitRadius = .025f;
        [SerializeField] private float bulletImpactPower = 10f;
        [SerializeField] private Tracer tracer;
        [SerializeField] private LayerMask collisionLayer = AllLayers;
        [SerializeField] private HitEffectCueSheet hitEffect;
        private IObjectPool<Tracer> _tracerPool;

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

            if (hit.transform.TryGetComponent(out IObjectMaterial material))
            {
                hitEffect.NullCast()?.Play(material.GetMaterial(hit.point), hit.point, hit.normal, hit.transform);
            }

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
        }
    }
}