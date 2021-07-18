using System;

namespace WeaponSystem.Scripts.Runtime
{
    [Serializable]
    public class GenericKeyValue<TKey, TValue>
    {
        public TKey key;
        public TValue value;
    }
}