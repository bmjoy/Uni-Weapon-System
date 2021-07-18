using System;
using ObjectPool;
using UnityEngine;

namespace WeaponSystem.Effect
{
    [Serializable]
    public class BuiltinDecal : IEffect
    {
        [SerializeField] private Projector projector;
        private IObjectPool<Projector> _decalPool;
        public bool IsValid => projector;

        public void Play(Vector3 position, Quaternion rotate, Transform parent)
        {
        }
    }
}