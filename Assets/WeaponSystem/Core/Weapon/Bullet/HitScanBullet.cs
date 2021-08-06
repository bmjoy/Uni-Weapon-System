using System;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Core.Collision;
using WeaponSystem.Core.Collision.ObjectMaterial;
using WeaponSystem.Core.Weapon.Bullet.Ammo;
using static UnityEngine.Physics;
using Object = UnityEngine.Object;

namespace WeaponSystem.Core.Weapon.Bullet
{
    [Serializable, AddTypeMenu("HitScan")]
    public class HitScanBullet : IBullet
    {
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private float hitRadius = .025f;
        [SerializeField] private float bulletImpactPower = 10f;
        [SerializeField] private LayerMask collisionLayer = AllLayers;
        [SerializeField] private Tracer tracer;

        public UnityEvent<ObjectInfo> onHit;
        public UnityEvent<RaycastHit> onSelfHit;
        public UnityEvent<RaycastHit> onFriendlyHit;
        public UnityEvent<RaycastHit> onEnemyHit;

        public void Shot(Vector3 position, Vector3 direction, IObjectPermission permission, IObjectGroup group)
        {
            var ray = new Ray(position, direction);
            
            if (SphereCast(ray, hitRadius, out RaycastHit hit, bulletConfig.MaxDistance, collisionLayer) == false)
            {
                if (tracer != null)
                {
                    Object.Instantiate(tracer, position, Quaternion.identity).SetPosition(position, direction * 1000f);
                }

                return;
            }
            
            if (tracer != null)
            {
                Object.Instantiate(tracer, position, Quaternion.identity).SetPosition(position, hit.point);
            }

            var info = hit.collider.TryGetComponent(out IObjectMaterial material)
                ? new ObjectInfo(material, hit.transform, hit.normal)
                : new ObjectInfo(null, hit.transform, hit.normal);

            onHit.Invoke(info);

            if (hit.collider.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddForce(-hit.normal * bulletImpactPower, ForceMode.Impulse);
            }

            if (group == null || permission == null) return;

            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                if (damageable.ObjectGroup.SelfId == group.SelfId)
                {
                    onSelfHit.Invoke(hit);

                    if (permission.SelfDamage)
                    {
                        damageable.AddDamage(bulletConfig.GetDamage(damageable.HitType, hit.distance));
                    }
                }

                if (damageable.ObjectGroup.GroupId == group.GroupId)
                {
                    onFriendlyHit.Invoke(hit);

                    if (permission.TeamDamage)
                    {
                        damageable.AddDamage(bulletConfig.GetDamage(damageable.HitType, hit.distance));
                    }
                }

                if (damageable.ObjectGroup.GroupId != group.GroupId)
                {
                    onEnemyHit.Invoke(hit);

                    if (permission.EnemyDamage)
                    {
                        damageable.AddDamage(bulletConfig.GetDamage(damageable.HitType, hit.distance));
                    }
                }
            }
        }
    }
}