using AudioSystem;
using AudioSystem.ObjectMaterial;
using UnityEngine;
using WeaponSystem.Collision;
using WeaponSystem.Effect;

namespace WeaponSystem.Weapon.Bullet
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    [AddComponentMenu("WeaponSystem/RifleAmmo")]
    public class RifleAmmo : ProjectileAmmo
    {
        [SerializeField] private BulletConfig config;
        [SerializeField] private HitEffectCueSheet hitEffect;
        private Rigidbody _rigidbody;

        public override IObjectPermission ObjectPermission { get; set; }
        public override IObjectGroup ObjectGroup { get; set; }
        public override void AddForce(Vector3 force) => _rigidbody.AddForce(force, ForceMode.VelocityChange);

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        private void OnEnable() => _rigidbody.Sleep();

        private void OnCollisionEnter(UnityEngine.Collision target)
        {
            var contact = target.contacts[0];
            
            if (target.transform.TryGetComponent(out IObjectMaterial material))
            {
                hitEffect.Play(material.GetMaterial(contact.point), contact.point, contact.normal, target.transform);
            }

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

            gameObject.SetActive(false);
        }
    }
}