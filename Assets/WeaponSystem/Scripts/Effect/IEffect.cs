using JetBrains.Annotations;
using UnityEngine;

namespace WeaponSystem.Effect
{
    public interface IEffect
    {
        bool IsValid { get; }
        void Play(Vector3 position, Quaternion rotate, [CanBeNull] Transform parent);
    }
}