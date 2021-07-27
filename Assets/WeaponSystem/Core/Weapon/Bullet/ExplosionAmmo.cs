using UnityEngine;
using WeaponSystem.Core.Collision;
using WeaponSystem.Core.Collision.ObjectMaterial;
using WeaponSystem.Core.Debug;
using WeaponSystem.Effect;

namespace WeaponSystem.Core.Weapon.Bullet
{
    [RequireComponent(typeof(ConstantForce), typeof(Collider))]
    public class ExplosionAmmo : ProjectileAmmo
    {
        // damage
        [SerializeField] private float explosionRadius;
        [SerializeField] private float explosionForce = 20f;
        [SerializeField] private BulletConfig config;

        // effect
        [SerializeReference, SubclassSelector] private IEffect _explosionEffect = new NoneEffect();
        [SerializeField] private HitEffectCueSheet hitEffect;


        private Transform _self;
        private ConstantForce _force;
        public override IObjectPermission ObjectPermission { get; set; }
        public override IObjectGroup ObjectGroup { get; set; }

        public override void AddForce(Vector3 force) => _force.force = force;

        private void Awake()
        {
            _self = transform;
            _force = GetComponent<ConstantForce>();
        }

        private void OnCollisionEnter(UnityEngine.Collision target)
        {
            Collider[] colliders = { };
            Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, colliders);
            
            

            CheckColliders(colliders);
            // gameObject.SetActive(false);
        }

        void CheckColliders(Collider[] colliders)
        {
            foreach (var c in colliders)
            {
                c.name.Log();
                
                if (c.TryGetComponent(out Rigidbody rigidbody))
                {
                    rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
                
                
            }
        }

        void PlayEffect(UnityEngine.Collision target)
        {
            if (hitEffect == null) return;
            var contact = target.contacts[0];
            if (target.transform.TryGetComponent(out IObjectMaterial material))
            {
                hitEffect.Play(material.GetMaterial(contact.point), contact.point, contact.normal, target.transform);
            }

            _explosionEffect?.Play(_self.position, Quaternion.identity, target.transform);
        }

        private void OnDrawGizmos() => Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}