using System;
using UnityEngine;

namespace WeaponSystem.Effect
{
    [Serializable]
    public sealed class NoneEffect : IEffect
    {
        public bool IsValid => false;
        public void Play(Vector3 position, Quaternion rotate, Transform parent) { }

        public void StopOrPlay(Vector3 position, Quaternion rotate, Transform parent) { }

        public void Stop() { }
    }
}