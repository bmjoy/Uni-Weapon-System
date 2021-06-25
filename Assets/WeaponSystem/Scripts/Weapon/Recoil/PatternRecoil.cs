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
        [SerializeField] private float kickPower = 2f;
        [SerializeField] private float easingDuration = .1f;
        [SerializeField] private float rollBackSpeed;
        private int _index;
        private float _time;

        void IRecoil.Reset()
        {
            _index = 0;
            var rotate = Locator<ICameraRotate>.Instance.Current;
            var rollbackVelocity = 0f;
            rotate.VerticalOffset =
                SmoothDampAngle(rotate.VerticalOffset, 0f, ref rollbackVelocity,
                    Time.deltaTime / easingDuration);
            var t = Time.deltaTime / rollBackSpeed;
            rotate.HorizontalOffset =
                SmoothDampAngle(rotate.HorizontalOffset, 0f, ref rollbackVelocity, Time.deltaTime / rollBackSpeed);
        }

        void IRecoil.Generate()
        {
            _time = easingDuration;
            _index++;
            kickPower = Abs(kickPower);
        }

        void IRecoil.Easing()
        {
            var rotate = Locator<ICameraRotate>.Instance.Current;

            if (rotate == null) return;
            if (_time < 0f) return;
            
            rotate.HorizontalOffset += patternData[_index].x *  kickPower * Time.deltaTime / easingDuration;
            rotate.VerticalOffset += patternData[_index].y * kickPower * Time.deltaTime / easingDuration;
            _time -= Time.deltaTime;
        }
    }
}