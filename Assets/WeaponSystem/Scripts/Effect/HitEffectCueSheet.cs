using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem.Effect
{
    [CreateAssetMenu(fileName = "new HitEffectCueSheet", menuName = "WeaponSystem/HitEffectCueSheet", order = 0)]
    public class HitEffectCueSheet : ScriptableObject
    {
        [SerializeField] private List<EffectKeyValuePair> hitEffects;

        public void Play(string name, Vector3 point, Vector3 normal, Transform parent)
        {
            foreach (var hitEffect in hitEffects)
            {
                if (name == hitEffect.key && hitEffect.value.IsValid)
                {
                    hitEffect.value.Play(point, Quaternion.LookRotation(normal), parent);
                }
            }
        }
    }
}