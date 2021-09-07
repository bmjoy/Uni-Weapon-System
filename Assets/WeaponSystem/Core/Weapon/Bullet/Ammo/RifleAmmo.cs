using UnityEngine;
using WeaponSystem.Core.Collision;
using WeaponSystem.Core.Collision.ObjectMaterial;


namespace WeaponSystem.Core.Weapon.Bullet.Ammo
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    [AddComponentMenu("WeaponSystem/RifleAmmo")]
    public class RifleAmmo : ProjectileAmmo
    {
        [SerializeField] private BulletDamageProfile damageProfile;
        private Rigidbody _rigidbody;
        private Vector3 _startPosition;
        public override IObjectPermission ObjectPermission { get; set; }
        public override IObjectGroup ObjectGroup { get; set; }

        public override void AddForce(Vector3 force) => _rigidbody.AddForce(force, ForceMode.VelocityChange);


        protected override void OnHitObject(UnityEngine.Collision target)
        {
            if (target.collider.TryGetComponent(out IDamageable damageable))
            {
                var distance = Mathf.Abs(Vector3.Distance(target.transform.position, _startPosition));


                if (damageable.ObjectGroup.SelfId == ObjectGroup.SelfId && ObjectPermission.SelfDamage)
                {
                    damageable.AddDamage(damageProfile.GetDamage(damageable.BodyType, distance));
                }

                if (damageable.ObjectGroup.GroupId == ObjectGroup.GroupId && ObjectPermission.TeamDamage)
                {
                    damageable.AddDamage(damageProfile.GetDamage(damageable.BodyType, distance));
                }

                if (damageable.ObjectGroup.GroupId != ObjectGroup.GroupId && ObjectPermission.EnemyDamage)
                {
                    damageable.AddDamage(damageProfile.GetDamage(damageable.BodyType, distance));
                }
            }
        }


        private void Awake() => _rigidbody = GetComponent<Rigidbody>();


        private void OnEnable()
        {
            _startPosition = transform.position;
            _rigidbody.Sleep();
        }
    }
}