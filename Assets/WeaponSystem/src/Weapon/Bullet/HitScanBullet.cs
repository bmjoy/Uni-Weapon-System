using UnityEngine;
using WeaponSystem.Runtime;
using static UnityEngine.Physics;

namespace WeaponSystem.Weapon.Bullet
{
    [System.Serializable]
    public class HitScanBullet : IBullet
    {
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private float bulletImpactPower = 10f;
        [SerializeField] private LayerMask collisionLayer = AllLayers;

        [Space(10)] [Header("Effect")] [SerializeField]
        private TrailRenderer bulletTrail;

        private ObjectPool<TrailRenderer> _trailObjectPool;

        [SerializeField] private ParticleSystem hitEffect;

        // ReSharper disable Unity.PerformanceAnalysis
        public void Shot(Vector3 position, Vector3 direction)
        {
            var ray = new Ray(position, direction);

            // hit
            if (Raycast(ray, out RaycastHit hit, bulletConfig.MaxDistance, collisionLayer) == false)
            {
                // none hit
                var dist = direction * (bulletConfig.MaxDistance > 0f ? bulletConfig.MaxDistance : 10f);
                GenerateTrail(position, dist);
                return;
            }

            GenerateHitEffect(hit.point, hit.normal, hit.transform);
            GenerateTrail(position, hit.point);

#if DEBUG
            Debug.Log(hit.collider.name);
#endif

            if (hit.collider.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddForce(-hit.normal * bulletImpactPower * bulletConfig.GetImpact(hit.distance),
                    ForceMode.Impulse);
            }
        }

        private void GenerateTrail(Vector3 start, Vector3 end)
        {
            if (bulletTrail == false) return;
            _trailObjectPool ??=
                new ObjectPool<TrailRenderer>(bulletTrail, parent: new GameObject("Trail Pool").transform);
            var trail = _trailObjectPool.GetObject();
            trail.transform.position = start;
            trail.gameObject.SetActive(true);
            trail.transform.position = end;
            trail.gameObject.SetActive(false);
        }

        private void GenerateHitEffect(Vector3 position, Vector3 normal, Transform parent)
        {
            if (hitEffect == false) return;
            Object.Instantiate(hitEffect, position, Quaternion.LookRotation(normal), parent);
        }
    }
}