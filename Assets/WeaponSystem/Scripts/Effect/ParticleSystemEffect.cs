using System;
using ObjectPool;
using UnityEngine;

namespace WeaponSystem.Effect
{
    [Serializable]
    public class ParticleSystemEffect : IEffect
    {
        [SerializeField] private ParticleSystem particle;
        private IObjectPool<ParticleSystem> _particlePool;

        public bool IsValid => particle;

        public void Play(Vector3 position, Quaternion rotate, Transform parent)
        {
            if (IsValid == false) return;
            _particlePool ??= new ObjectPool<ParticleSystem>(particle, 10);
            var effect = _particlePool.GetObject(position, rotate, parent);
            effect.Stop();
            effect.gameObject.SetActive(true);
            effect.Play();
        }
    }
}