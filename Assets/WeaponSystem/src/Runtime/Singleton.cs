using System;

namespace WeaponSystem.Runtime
{
    public class Singleton<T>
    {
        public static Singleton<T> Instance => _instance.Value;
        private static Lazy<Singleton<T>> _instance = new Lazy<Singleton<T>>(() => new Singleton<T>());
        protected Singleton() { }
    }
}