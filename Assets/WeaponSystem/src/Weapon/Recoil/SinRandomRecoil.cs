﻿using UnityEngine;
using static UnityEngine.Mathf;
using WeaponSystem.Camera;
using WeaponSystem.Runtime;


namespace WeaponSystem.Weapon.Recoil
{
    [System.Serializable, AddTypeMenu("SinRandom")]
    public class SinRandomRecoil : IRecoil
    {
        [SerializeField, Range(1f, 50f)] private float distance = 2f;
        [SerializeField] private float horizontalFrequency = 25f;
        [SerializeField] private float horizontalWidth = 2f;
        [SerializeField] private AnimationCurve horizontalWeightCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        [SerializeField] private AnimationCurve verticalWeightCurve = AnimationCurve.Constant(0f, 1f, 1f);
        [SerializeField] private float easingDuration = .1f;

        private float _easeTime;
        private float _hRecoil;
        private float _vRecoil;

        public void Reset()
        {
            var rotate = Locator<ICameraRotate>.Instance.Current;
            var spd = 0f;
            rotate.VerticalOffset =
                SmoothDampAngle(rotate.VerticalOffset, 0f, ref spd, Time.deltaTime / easingDuration);
            rotate.HorizontalOffset =
                SmoothDampAngle(rotate.HorizontalOffset, 0f, ref spd, Time.deltaTime / easingDuration);
        }

        public void Generate()
        {
            _easeTime = easingDuration;
            _hRecoil = GenerateHorizontalRecoil();
            _vRecoil = GenerateVerticalRecoil();
        }

        public void Easing()
        {
            var rotate = Locator<ICameraRotate>.Instance.Current;

            if (rotate == null) return;

            if (_easeTime < 0f) return;

            rotate.HorizontalOffset += _hRecoil * Time.deltaTime / easingDuration;
            rotate.VerticalOffset += _vRecoil * Time.deltaTime / easingDuration;
            _easeTime -= Time.deltaTime;
        }

        private float GenerateHorizontalRecoil()
        {
            var horizontal = Sin(Time.time * (PI * horizontalFrequency));
            horizontal *= horizontalWeightCurve.Evaluate(Random.value);
            return horizontal * (1f / distance) * horizontalWidth;
        }

        private float GenerateVerticalRecoil()
        {
            var vertical = verticalWeightCurve.Evaluate(Random.value);
            return vertical * (1f / distance);
        }
    }
}