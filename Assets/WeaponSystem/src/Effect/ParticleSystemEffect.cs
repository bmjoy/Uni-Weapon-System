using System;
using UnityEngine;

namespace WeaponSystem.Effect
{
    [Serializable]
    public class ParticleSystemEffect : IEffect
    {
        [SerializeField] private ParticleSystem particle;

        public bool IsValid => particle;

        public void Play(Vector3 position, Quaternion rotate, Transform parent)
        {
            if (IsValid == false) return;
            particle.Play();
        }

        public void StopOrPlay(Vector3 position, Quaternion rotate, Transform parent)
        {
            if (IsValid == false) return;
            if (particle.isPlaying)
            {
                Stop();
            }
            else
            {
                Play(position, rotate, parent);
            }
        }

        public void Stop()
        {
            if (IsValid == false) return;
            if (particle) particle.Stop();
        }
    }
}