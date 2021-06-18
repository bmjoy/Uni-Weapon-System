using System;
using UnityEngine;
using WeaponSystem.Collision;
using static UnityEngine.Physics;

namespace WeaponSystem.Weapon.Bullet
{
    [Serializable, AddTypeMenu("HitScan")]
    public class HitScanBullet : IBullet
    {
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private float bulletImpactPower = 10f;
        [SerializeField] private LayerMask collisionLayer = AllLayers;

        // ReSharper disable Unity.PerformanceAnalysis
        public void Shot(Vector3 position, Vector3 direction, IObjectPermission permission, IObjectGroup group)
        {
            var ray = new Ray(position, direction);

            // hit
            if (Raycast(ray, out RaycastHit hit, bulletConfig.MaxDistance, collisionLayer) == false)
            {
                // none hit
                var dist = direction * (bulletConfig.MaxDistance > 0f ? bulletConfig.MaxDistance : 10f);
                return;
            }

            if (hit.collider.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddForce(-hit.normal * bulletImpactPower * bulletConfig.GetImpact(hit.distance),
                    ForceMode.Impulse);
            }
        }
    }
}