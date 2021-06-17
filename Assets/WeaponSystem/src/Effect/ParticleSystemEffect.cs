using UnityEngine;

namespace WeaponSystem.Effect
{
    public class ParticleSystemEffect : IEffect
    {
        public void Play(Vector3 position, Quaternion rotate, Transform parent)
        {
            
        }

        public void StopOrPlay(Vector3 position, Quaternion rotate, Transform parent)
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}