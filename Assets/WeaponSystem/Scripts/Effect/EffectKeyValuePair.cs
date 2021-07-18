using UnityEngine;
using WeaponSystem.Scripts.Attribute;

namespace WeaponSystem.Effect
{
    [System.Serializable]
    public class EffectKeyValuePair
    {
        [TagField] public string key;

        [SerializeReference, SubclassSelector] public IEffect value = new ParticleSystemEffect();
    }
}