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
        [SerializeField] private float lifeTime = 1f;

        [SerializeField] private HitEffectConfig hitEffect;
        private float _lifeTimeCounter;

        private Rigidbody _rigidbody;


        public override IObjectPermission ObjectPermission { get; set; }
        public override IObjectGroup ObjectGroup { get; set; }
        public override void AddForce(Vector3 force) => _rigidbody.AddForce(force, ForceMode.VelocityChange);

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable() => _rigidbody.Sleep();

        private void OnDisable() => _lifeTimeCounter = 0f;

        // Update is called once per frame
        private void Update()
        {
            if (_lifeTimeCounter < lifeTime)
            {
                _lifeTimeCounter += Time.deltaTime;
                return;
            }

            gameObject.SetActive(false);
        }

        private void OnCollisionEnter(UnityEngine.Collision target)
        {
            Debug.Log("hit");
            var contact = target.contacts[0];

            hitEffect.Play(target.transform, contact.point, contact.normal);

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