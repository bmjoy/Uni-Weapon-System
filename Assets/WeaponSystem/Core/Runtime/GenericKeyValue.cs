using System;

namespace WeaponSystem.Core.Runtime
{
    [Serializable]
    public class GenericKeyValue<TKey, TValue>
    {
        public TKey key;
        public TValue value;
    }
}