using AudioSystem;
using UnityEngine;
using WeaponSystem.Collision;
using WeaponSystem.Effect;


namespace WeaponSystem.Weapon.Bullet
{
    [RequireComponent(typeof(ConstantForce), typeof(Collider))]
    public class RocketAmmo : ProjectileAmmo
    {
        [SerializeField] private float radius;
        [SerializeField] private float force = 2f;
        [SerializeField] private HitEffectCueSheet hitEffect;
        [SerializeField] private BulletConfig config;
        public override IObjectPermission ObjectPermission { get; set; }
        public override IObjectGroup ObjectGroup { get; set; }

        public override void AddForce(Vector3 force) => _force.force = force;

        private ConstantForce _force;

        private void Awake() => _force = GetComponent<ConstantForce>();

        private void OnCollisionEnter(UnityEngine.Collision target)
        {
            var contact = target.contacts[0];
            
            if (target.transform.TryGetComponent(out IObjectMaterial material))
            {
                hitEffect.Play(material.GetMaterial(contact.point), contact.point, contact.normal, target.transform);
            }
            
            Collider[] colliders = { };
            Physics.OverlapSphereNonAlloc(transform.position, radius, colliders);

            foreach (var c in colliders)
            {
                if (c.TryGetComponent(out Rigidbody rigidbody))
                {
                    rigidbody.AddExplosionForce(force, transform.position, radius);
                }

                if (c.TryGetComponent(out IDamageable damageable))
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
        }
    }
}