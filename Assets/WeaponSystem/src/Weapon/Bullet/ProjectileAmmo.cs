using UnityEngine;
using WeaponSystem.Collision;
using WeaponSystem.Scripts.Runtime;

namespace WeaponSystem.Weapon.Bullet
{
    [AddComponentMenu("WeaponSystem/ProjectileAmmo"), RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class ProjectileAmmo : MonoBehaviour
    {
        [SerializeField] private BulletConfig config;
        [SerializeField] private float lifeTime = 1f;
        [SerializeField] private ParticleSystem[] hitEffects;
        private float _lifeTimeCounter;
        private Vector3 _startPos;
        private void OnDisable() => _lifeTimeCounter = 0f;

        private void OnEnable() => _startPos = transform.position;

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
                var dist = (transform.position - _startPos).magnitude;
                Debug.Log($"Dist: {dist.ToString()}");
                damageable.AddDamage(config.GetDamage(damageable.HitType));
            }

            foreach (var hitEffect in hitEffects) hitEffect.NullCast()?.Play();

            gameObject.SetActive(false);
        }
    }
}