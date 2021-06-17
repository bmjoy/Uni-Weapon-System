using JetBrains.Annotations;
using UnityEngine;

namespace WeaponSystem.Effect
{
    public interface IEffect
    {
        void Play(Vector3 position, Quaternion rotate, [CanBeNull] Transform parent);
        void StopOrPlay(Vector3 position, Quaternion rotate, [CanBeNull] Transform parent);
        void Stop();
    }
}