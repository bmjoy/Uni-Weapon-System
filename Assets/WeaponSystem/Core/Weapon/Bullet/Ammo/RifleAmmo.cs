using UnityEngine;
using WeaponSystem.Core.Collision;
using WeaponSystem.Core.Collision.ObjectMaterial;

namespace WeaponSystem.Core.Weapon.Bullet.Ammo
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    [AddComponentMenu("WeaponSystem/RifleAmmo")]
    public class RifleAmmo : ProjectileAmmo
    {
        [SerializeField] private BulletConfig config;
        private Rigidbody _rigidbody;

        public override IObjectPermission ObjectPermission { get; set; }
        public override IObjectGroup ObjectGroup { get; set; }
        public override void AddForce(Vector3 force) => _rigidbody.AddForce(force, ForceMode.VelocityChange);

        protected override void OnHitObject(UnityEngine.Collision target)
        {
            if (target.collider.TryGetComponent(out IDamageable damageable))
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
        }

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        private void OnEnable() => _rigidbody.Sleep();
    }
}