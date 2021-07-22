﻿using System;
using UnityEngine;
using WeaponSystem.Camera;
using WeaponSystem.Runtime;
using Random = UnityEngine.Random;

namespace WeaponSystem.Weapon.Recoil
{
    [Serializable, AddTypeMenu("Sin")]
    public class SinRecoil : IRecoil
    {
        [SerializeField] private float duration;
        [SerializeField] private float frequency = 2f;
        [SerializeField] private float power = 10f;

        private float _easeTime;

        public void Reset()
        {
            if (_easeTime > 0) return;
            var rotate = Locator<ICameraFixedStar>.Instance.Current;
            if (rotate == null) return;
            

            rotate.HorizontalOffset = Mathf.Lerp(rotate.HorizontalOffset, 0f, Time.deltaTime / duration);
            rotate.VerticalOffset = Mathf.Lerp(rotate.VerticalOffset, 0f, Time.deltaTime / duration);
        }

        public void Generate() => _easeTime = duration;

        public void Easing()
        {
            if (_easeTime < 0f) return;
            var rotate = Locator<ICameraFixedStar>.Instance.Current;
            if (rotate == null) return;

            _easeTime -= Time.deltaTime;
            rotate.HorizontalOffset += Mathf.Sin(Time.time * Mathf.PI * frequency) * power * Time.deltaTime;
            rotate.VerticalOffset += Random.value * power * Time.deltaTime;
        }
    }
}