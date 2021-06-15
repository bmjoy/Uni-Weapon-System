using System;
using UnityEngine;
using WeaponSystem.Camera;
using WeaponSystem.Runtime;
using static UnityEngine.Mathf;
using Random = UnityEngine.Random;

namespace WeaponSystem.Weapon.Recoil
{
    [Serializable]
    public class TRecoil : IRecoil
    {
        [SerializeField] private float duration = .1f;
        [SerializeField] private float horizontalFrequency = .5f;
        [SerializeField] private int verticalCount = 5;


        private float _hRecoil, _vRecoil;
        private float _time;
        private int _count;

        public void Reset() => _count = 0;

        public void Generate()
        {
            _hRecoil = Sin(Time.time * (PI * horizontalFrequency)) * Deg2Rad;

            _vRecoil = 0f;
            if (_count < verticalCount)
            {
                _vRecoil = Clamp(Random.value, .5f, 1f);
                _count++;
            }

            _time = duration;
        }

        public void Easing()
        {
            var rotate = Locator<ICameraRotate>.Instance.Current;

            if (rotate == null) return;
            if (_time < 0f) return;

            rotate.Horizontal += (_hRecoil * Time.deltaTime) / duration;
            rotate.Vertical += (_vRecoil * Time.deltaTime) / duration;
            _time -= Time.deltaTime;
        }
    }
}