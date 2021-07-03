using Cinemachine;
using UnityEngine;

namespace WeaponSystem.Effect
{
    [System.Serializable]
    public class EffectKeyValuePair
    {
        [TagField] public string key;

        [SerializeReference, SubclassSelector] public IEffect value = new ParticleSystemEffect();
    }
}