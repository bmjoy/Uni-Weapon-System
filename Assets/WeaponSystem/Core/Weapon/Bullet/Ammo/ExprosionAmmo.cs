using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Core.Collision;
using WeaponSystem.Core.Debug;

namespace WeaponSystem.Core.Weapon.Bullet.Ammo
{
    public class ExprosionAmmo : ProjectileAmmo
    {
        [SerializeField] private float exprosionRadius = 2.5f;
        [SerializeField] private float exprosionTime = .5f;
        [SerializeField] private float exprosionForce = 100f;
        [SerializeField] private LayerMask collisionMask = Physics.AllLayers;
        [SerializeField] private bool isSticky;
        [SerializeField] private BulletConfig config;
        private Rigidbody _rigidbody;

        public UnityEvent onExprosion;
        private Transform _self;

        private WaitForSeconds _seconds;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _self = transform;
        }

        public override IObjectPermission ObjectPermission { get; set; }
        public override IObjectGroup ObjectGroup { get; set; }
        public override void AddForce(Vector3 force) => _rigidbody.AddForce(force, ForceMode.VelocityChange);


        protected override void OnHitObject(UnityEngine.Collision target)
        {
            StartCoroutine(Exprosion());
            if (isSticky)
            {
                _self.parent = target.transform;
                _rigidbody.Sleep();
            }
        }

        private IEnumerator Exprosion()
        {
            yield return _seconds ??= new WaitForSeconds(exprosionTime);


            var colliders = Physics.OverlapSphere(_self.position, exprosionRadius, collisionMask);

            foreach (var col in colliders)
            {
                col.name.Log();
                if (col.TryGetComponent(out IDamageable damageable))
                {
                    if (damageable.ObjectGroup.SelfId == ObjectGroup.SelfId && ObjectPermission.SelfDamage)
                    {
                        damageable.AddDamage(config.GetDamage(damageable.HitType));
                    }

                    if (damageable.ObjectGroup.GroupId == ObjectGroup.GroupId && ObjectPermission.TeamDamage)
                    {
                        damageable.AddDamage(config.GetDamage(damageable.HitType));
                    }

                    if (damageable.ObjectGroup.GroupId != ObjectGroup.GroupId && ObjectPermission.EnemyDamage)
                    {
                        damageable.AddDamage(config.GetDamage(damageable.HitType));
                    }
                }

                if (col.TryGetComponent(out Rigidbody rb))
                {
                    rb.AddExplosionForce(exprosionForce, _self.position, exprosionRadius);
                }
            }

            onExprosion.Invoke();
        }

        private void OnDrawGizmos() => Gizmos.DrawWireSphere(_self.position, exprosionRadius);
    }
}