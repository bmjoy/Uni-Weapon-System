using UnityEngine;

namespace WeaponSystem.Core.ObjectPool
{
    public interface IObjectPoolFactory
    {
        IObjectPool<T> CreatePool<T>(T prefab, int preInstantiate) where T : Component;
    }
}