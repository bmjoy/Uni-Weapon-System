using System;
using UnityEngine;
using WeaponSystem.Attribute;

namespace WeaponSystem.Core.Runtime.Timer
{
    [Serializable, AddTypeMenu("Second")]
    public class SecondBasedTimer : IRpmTimer
    {
        [SerializeField, Positive] private float waitTime = 1f;
        private float _intervalCounter;
        public bool IsValid => _intervalCounter > waitTime;

        public void Update() => _intervalCounter += IsValid ? 0f : Time.deltaTime;

        public void Reset() { }

        public void Lap() => _intervalCounter = 0f;
    }
}