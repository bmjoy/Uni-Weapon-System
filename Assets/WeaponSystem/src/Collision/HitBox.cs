using UnityEngine;

namespace WeaponSystem.Collision
{
    [RequireComponent(typeof(Collider))]
    public class HitBox : MonoBehaviour, IDamageable
    {
        [SerializeField] private HitType hitType;
        [SerializeField] private DamageCollision damageCollision;
        private IHasHitPoint _hasHitPoint;
        public DamageCollision DamageCollision => damageCollision;
        public HitType HitType => hitType;

        private void Awake() => _hasHitPoint = transform.GetComponentInParent<IHasHitPoint>();

        public void AddDamage(float damage)
        {
            _hasHitPoint ??= transform.GetComponentInParent<IHasHitPoint>();
            Debug.Log($"Hit: {damage.ToString()}");
            _hasHitPoint?.AddDamage(damage);
        }
    }
}