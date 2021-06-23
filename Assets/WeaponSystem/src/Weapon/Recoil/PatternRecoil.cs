using UnityEngine;
using WeaponSystem.Camera;
using WeaponSystem.Runtime;
using static UnityEngine.Mathf;
using System;

namespace WeaponSystem.Weapon.Recoil
{
    [Serializable, AddTypeMenu("Pattern")]
    public class PatternRecoil : IRecoil
    {
        [SerializeField] private RecoilPatternData patternData;
        [SerializeField] private float distance = 2f;
        [SerializeField] private float easingDuration = .1f;
        private int _index;
        private float _time;

        public void Reset()
        {
            _index = 0;
            var rotate = Locator<ICameraRotate>.Instance.Current;
            var rollbackVelocity = 0f;
            rotate.VerticalOffset =
                SmoothDampAngle(rotate.VerticalOffset, 0f, ref rollbackVelocity, Time.deltaTime / easingDuration);
            rotate.HorizontalOffset =
                SmoothDampAngle(rotate.HorizontalOffset, 0f, ref rollbackVelocity, Time.deltaTime / easingDuration);
        }

        public void Generate()
        {
            _time = easingDuration;
            _index++;
        }

        public void Easing()
        {
            var rotate = Locator<ICameraRotate>.Instance.Current;

            if (rotate == null) return;
            if (_time < 0f) return;

            rotate.Horizontal += patternData[_index].x * (1f / distance) * Time.deltaTime / easingDuration;
            rotate.Vertical += patternData[_index].y * (1f / distance) * Time.deltaTime / easingDuration;
            _time -= Time.deltaTime;
        }
    }
}