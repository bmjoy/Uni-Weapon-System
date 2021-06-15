using System;
using UnityEngine;

namespace WeaponSystem.Runtime
{
    [Serializable]
    public class IntervalCounter
    {
        private float _intervalTime;
        private float _counter;

        public bool IsValid => _counter > IntervalTime;

        public float IntervalTime
        {
            get => _intervalTime;
            set => _intervalTime = Mathf.Clamp(value, 0f, Single.MaxValue);
        }


        public void Update()
        {
            if (IsValid) return;
            _counter += Time.deltaTime;
        }

        public void Reset() => _counter = 0f;
    }
}