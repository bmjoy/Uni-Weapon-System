using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem.Effect
{
    [CreateAssetMenu(fileName = "new HitEffectConfig", menuName = "WeaponSystem/HitEffectConfig", order = 0)]
    public class HitEffectConfig : ScriptableObject
    {
        [SerializeField] private List<EffectKeyValuePair> hitEffects;

        public void Play(Transform transform, Vector3 point, Vector3 normal)
        {
            foreach (var hitEffect in hitEffects)
            {
                if (transform.CompareTag(hitEffect.key) && hitEffect.value.IsValid)
                {
                    hitEffect.value.Play(point, Quaternion.LookRotation(normal), transform);
                }
            }
        }
    }
}