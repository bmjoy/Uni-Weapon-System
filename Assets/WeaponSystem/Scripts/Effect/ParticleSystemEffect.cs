using System;
using UnityEngine;
using WeaponSystem.Runtime;

namespace WeaponSystem.Effect
{
    [Serializable]
    public class ParticleSystemEffect : IEffect
    {
        [SerializeField] private ParticleSystem particle;
        private ObjectPool<ParticleSystem> _particlePool;

        public bool IsValid => particle;

        public void Play(Vector3 position, Quaternion rotate, Transform parent)
        {
            if (IsValid == false) return;
            _particlePool ??= new ObjectPool<ParticleSystem>(particle, 10, parent);
            var effect = _particlePool.GetObject(position, rotate, parent);
            effect.gameObject.SetActive(true);
            effect.Play();
        }
    }
}