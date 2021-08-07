using UnityEngine;

namespace WeaponSystem.Core.ObjectPool
{
    [AddTypeMenu("Default")]
    public class DefaultObjectPoolFactory : IObjectPoolFactory
    {
        public IObjectPool<T> CreatePool<T>(T prefab, int preInstantiate) where T : Component
        {
            return new DefaultObjectPool<T>(prefab, preInstantiate);
        }
    }
}