using UnityEngine;

namespace WeaponSystem.Core.Collision
{
    [RequireComponent(typeof(Collider))]
    public class HitBox : MonoBehaviour, IDamageable
    {
        [SerializeField] private BodyType bodyType;
        private IHasHitPoint _hasHitPoint;
        private IObjectGroup _group;
        public BodyType BodyType => bodyType;
        public IObjectGroup ObjectGroup => _group;

        private void Awake()
        {
            _hasHitPoint = transform.GetComponentInParent<IHasHitPoint>();
            _group = transform.GetComponentInParent<IObjectGroup>();
        }

        public void AddDamage(float damage) => _hasHitPoint.AddDamage(damage);
    }
}