using UnityEngine;
using WeaponSystem.Camera;
using WeaponSystem.Runtime;
using System;

namespace WeaponSystem.Weapon.Recoil
{
    [Serializable, AddTypeMenu("Pattern")]
    public class PatternRecoil : IRecoil
    {
        [SerializeField] private RecoilPatternData patternData;
        [SerializeField] private float power = 2f;
        [SerializeField] private float duration = .1f;
        private int _index;
        private float _easeTime;

        public void Reset()
        {
            _index = 0;

            if (_easeTime > 0) return;
            var rotate = Locator<ICameraFixedStar>.Instance.Current;
            if (rotate == null) return;

            rotate.HorizontalOffset = Mathf.Lerp(rotate.HorizontalOffset, 0f, Time.deltaTime / duration);
            rotate.VerticalOffset = Mathf.Lerp(rotate.VerticalOffset, 0f, Time.deltaTime / duration);
        }

        public void Generate()
        {
            _index++;
            _easeTime = duration;
        }

        public void Easing()
        {
            var rotate = Locator<ICameraFixedStar>.Instance.Current;
            if (_easeTime < 0f) return;
            if (rotate == null) return;
            rotate.HorizontalOffset += patternData[_index].x * power * Time.deltaTime;
            rotate.VerticalOffset += patternData[_index].y * power * Time.deltaTime;
        }
    }
}