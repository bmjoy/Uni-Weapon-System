using UnityEngine;
using WeaponSystem.Core.Collision;

namespace WeaponSystem.Core.Weapon.Bullet
{
    [CreateAssetMenu(menuName = "WeaponSystem/BulletDamageProfile")]
    public class BulletDamageProfile : ScriptableObject
    {
        [SerializeField] private float maxDistance = 300f;
        [SerializeField] private AnimationCurve damageExtinctionCurve = AnimationCurve.Linear(0f, 1f, 1f, .85f);
        [SerializeField] private HitDamage[] damages;

        public float MaxDistance => maxDistance;

        public float GetDamage(BodyType bodyType, float distance = 0f)
        {
            foreach (var damage in damages)
            {
                if (bodyType == damage.bodyType) return damage.damage * GetImpact(distance);
            }

            return 0f;
        }

        public float GetImpact(float distance) => damageExtinctionCurve
            .Evaluate(Mathf.Clamp(distance, 0f, maxDistance) / maxDistance);
    }
}