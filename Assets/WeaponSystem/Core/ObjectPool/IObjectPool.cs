using System.Collections;
using UnityEngine;

namespace WeaponSystem.Core.ObjectPool
{
    public interface IObjectPool<out TComponent> : IEnumerable
    {
        TComponent GetObject();
        TComponent GetObject(Vector3 position, Quaternion rotate);
        TComponent GetObject(Vector3 position, Quaternion rotate, Transform parent);
        int PlayingCount { get; }
        int MaxPooling { get; set; }
        void Sleep();
        void Clear();
    }
}