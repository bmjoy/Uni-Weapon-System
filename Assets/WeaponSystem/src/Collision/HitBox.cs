using UnityEngine;

namespace WeaponSystem.Collision
{
    [RequireComponent(typeof(Collider))]
    public class HitBox : MonoBehaviour, IDamageable
    {
        [SerializeField] private HitType hitType;
        private IHasHitPoint _hasHitPoint;
        public HitType HitType => hitType;
        public IObjectGroup ObjectGroup { get; }

        private void Awake()
        {
            _hasHitPoint = transform.GetComponentInParent<IHasHitPoint>();
        }

        public void AddDamage(float damage)
        {
            _hasHitPoint ??= transform.GetComponentInParent<IHasHitPoint>();
            _hasHitPoint?.AddDamage(damage);
        }
    }
}