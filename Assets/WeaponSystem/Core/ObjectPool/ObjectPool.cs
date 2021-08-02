using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Object;

namespace WeaponSystem.Core.ObjectPool
{
    public class ObjectPool<TComponent> : IObjectPool<TComponent> where TComponent : Component
    {
        private TComponent _prefab;
        private List<TComponent> _observePrefab;
        private int _maxPooling;
        private int _lastPoped;

        public ObjectPool(TComponent prefab, int preInstantiate = 10)
        {
            _prefab = prefab;
            _observePrefab = new List<TComponent>(preInstantiate) {prefab};

            foreach (var _ in Enumerable.Range(0, preInstantiate))
            {
                var added = Instantiate(_prefab);
                added.gameObject.SetActive(false);
                _observePrefab.Add(added);
            }
        }

        public ObjectPool(Func<TComponent> valueFactory, int preInstantiate = 10)
            : this(valueFactory.Invoke(), preInstantiate) { }

        public ObjectPool(TComponent prefab, int preInstantiate = 10, Transform parent = null)
        {
            _prefab = prefab;
            _observePrefab = new List<TComponent>(preInstantiate) {prefab};

            foreach (var _ in Enumerable.Range(0, preInstantiate))
            {
                var added = Instantiate(_prefab, parent);
                added.gameObject.SetActive(false);
                _observePrefab.Add(added);
            }
        }

        public ObjectPool(Func<TComponent> valueFactory, int preInstantiate, Func<Transform> rootFactory) : this
            (valueFactory.Invoke(), preInstantiate, rootFactory.Invoke()) { }

        public ObjectPool(TComponent component, int preInstantiate = 10, int maxPooling = 0, Transform root = null)
            : this(component, preInstantiate < maxPooling ? maxPooling : preInstantiate) => _maxPooling = maxPooling;

        public int MaxPooling
        {
            get => _maxPooling;
            set => _maxPooling = value;
        }

        IEnumerator IEnumerable.GetEnumerator() => _observePrefab.GetEnumerator();

        public TComponent GetObject()
        {
            for (int i = 0; i < _observePrefab.Count; i++)
            {
                if (_observePrefab[i].gameObject.activeSelf == false)
                {
                    _lastPoped = i;
                    return _observePrefab[i];
                }
            }

            if (_maxPooling < PlayingCount && _maxPooling > 0)
            {
                _observePrefab[(_lastPoped + 3) % _observePrefab.Count].gameObject.SetActive(false);
                return _observePrefab[(_lastPoped + 3) % _observePrefab.Count];
            }


            var added = Instantiate(_prefab);
            _observePrefab.Add(added);
            return added;
        }

        public TComponent GetObject(Vector3 position, Quaternion rotate)
        {
            var getObj = GetObject();
            var transform = getObj.transform;
            transform.position = position;
            transform.rotation = rotate;
            return getObj;
        }

        public TComponent GetObject(Vector3 position, Quaternion rotate, Transform parent)
        {
            var getObj = GetObject(position, rotate);
            getObj.gameObject.SetActive(true);
            getObj.transform.parent = parent;
            return getObj;
        }

        public int PlayingCount => _observePrefab.Count(component => component.gameObject.activeSelf);

        public void Sleep()
        {
            foreach (var component in _observePrefab) component.gameObject.SetActive(false);
        }

        public void Clear()
        {
            foreach (var component in _observePrefab) Destroy(component);
            _observePrefab.Clear();
        }

        ~ObjectPool() => Clear();
    }
}