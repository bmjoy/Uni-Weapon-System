using UnityEngine;

namespace WeaponSystem.Collision
{
    [RequireComponent(typeof(Collider))]
    public class HitBox : MonoBehaviour, IDamageable
    {
        public HitType HitType => hitType;
        [SerializeField] private HitType hitType;
        private IHitPoint _hitPoint;
        
        private void Awake()
        {
            _hitPoint = transform.GetComponentInParent<IHitPoint>();
        }

        public void AddDamage(float damage)
        {
            _hitPoint.AddDamage(damage);
        }
    }
}
