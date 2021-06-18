using UnityEngine;
using WeaponSystem.Collision;
using WeaponSystem.Effect;

namespace WeaponSystem.Weapon.Bullet
{
    [AddComponentMenu("WeaponSystem/RifleAmmo"), RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class RifleAmmo : ProjectileAmmo
    {
        [SerializeField] private BulletConfig config;
        [SerializeField] private float lifeTime = 1f;
        [SerializeReference] private IEffect[] hitEffects;
        private float _lifeTimeCounter;

        private Rigidbody _rigidbody;

        public override IObjectPermission ObjectPermission { get; set; }
        public override IObjectGroup ObjectGroup { get; set; }
        public override Rigidbody Rigidbody => _rigidbody;

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

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
            Debug.Log($"collision {target.transform.name}");

            if (target.collider.TryGetComponent(out IDamageable damageable))
            {
                if (damageable.ObjectGroup.SelfId == ObjectGroup.SelfId && ObjectPermission.SelfDamage)
                {
                    return;
                }
                
                if (damageable.ObjectGroup.GroupId == ObjectGroup.GroupId && ObjectPermission.TeamDamage)
                {
                    return;
                }
                
                if (ObjectPermission.EnemyDamage)
                {
                    damageable.AddDamage(config.GetDamage(damageable.HitType));
                }
                
                damageable.AddDamage(config.GetDamage(damageable.HitType));
            }

            gameObject.SetActive(false);
        }
    }
}