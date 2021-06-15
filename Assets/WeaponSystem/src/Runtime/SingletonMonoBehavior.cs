using System;
using UnityEngine;

namespace WeaponSystem.Runtime
{
    public abstract class SingletonMonoBehavior<T> : MonoBehaviour, IDisposable where T : MonoBehaviour
    {
        public static T Instance => _instance == null ? _instance = Generate : _instance;
        private static T _instance;

        private static T Generate
        {
            get
            {
                _attachGameObject = new GameObject($"Singleton {typeof(T).Name}");
                DontDestroyOnLoad(_attachGameObject);
                return _attachGameObject.AddComponent<T>();
            }
        }

        protected SingletonMonoBehavior() { }

        private static GameObject _attachGameObject;

        public void Dispose() => Destroy(_attachGameObject);
    }
}