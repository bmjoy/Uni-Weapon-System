using UnityEngine;
using WeaponSystem.Collision;

namespace WeaponSystem.Weapon.Bullet
{
    [CreateAssetMenu(fileName = "New Bullet Config", menuName = "Weapon System/New Bullet Config", order = 0)]
    public class BulletConfig : ScriptableObject
    {
        [SerializeField] private float maxDistance = 300f;
        [SerializeField] private AnimationCurve damageExtinctionCurve = AnimationCurve.Linear(0f, 1f, 1f, .85f);
        [SerializeField] private HitDamage[] damages;

        public float MaxDistance => maxDistance;

        public float GetDamage(HitType hitType, float distance = 0f)
        {
            foreach (var damage in damages)
            {
                if (hitType == damage.hitType)
                {
                    return damage.damage * GetImpact(distance);
                }
            }

            return 0f;
        }

        public float GetImpact(float distance) => damageExtinctionCurve
            .Evaluate(Mathf.Clamp(distance, 0f, maxDistance) / maxDistance);
    }
}