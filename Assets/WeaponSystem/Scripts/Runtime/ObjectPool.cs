using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using static System.Linq.Enumerable;
using static UnityEngine.Object;

namespace WeaponSystem.Runtime
{
    public class ObjectPool<TComponent> where TComponent : Component
    {
        private TComponent _reference;
        private List<TComponent> _prefabs = new List<TComponent>();
        private Transform _parent = null;

        private ObjectPool() { }

        public ObjectPool(TComponent reference, int preInstantiate = 10, [CanBeNull] Transform parent = null)
        {
            _reference = reference;

            _parent = parent;

            foreach (var _ in Range(0, preInstantiate))
            {
                var addPrefab = Instantiate(_reference);
                addPrefab.gameObject.SetActive(false);
                _prefabs.Add(addPrefab);

                if (parent != null) addPrefab.transform.parent = parent;
            }
        }

        public TComponent GetObject()
        {
            foreach (var prefab in _prefabs)
            {
                if (prefab.gameObject.activeSelf == false)
                {
                    prefab.gameObject.SetActive(true);
                    return prefab;
                }
            }

            var newPrefab = _parent != null ? Instantiate(_reference, _parent) : Instantiate(_reference);
            _prefabs.Add(newPrefab);
            return newPrefab;
        }

        public TComponent GetObject(Transform parent)
        {
            var prefab = GetObject();
            var transform = prefab.transform;
            transform.parent = parent;
            transform.localPosition = Vector3.zero;
            return prefab;
        }

        public TComponent GetObject(Vector3 position)
        {
            var prefab = GetObject();
            prefab.transform.position = position;
            return prefab;
        }

        public TComponent GetObject(Vector3 position, Quaternion rotation)
        {
            var prefab = GetObject();
            var transform = prefab.transform;
            transform.position = position;
            transform.rotation = rotation;
            return prefab;
        }

        public TComponent GetObject(Vector3 position, Quaternion rotation, Transform parent)
        {
            var prefab = GetObject();
            var transform = prefab.transform;
            transform.parent = parent;
            transform.position = position;
            transform.rotation = rotation;
            return prefab;
        }

        public void Clear()
        {
            foreach (var prefab in _prefabs)
            {
                Destroy(prefab.gameObject);
            }

            _prefabs.Clear();
        }
    }
}