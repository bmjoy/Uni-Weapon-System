using System;
using UnityEngine;
using WeaponSystem.Camera;
using WeaponSystem.Runtime;
using Random = UnityEngine.Random;

namespace WeaponSystem.Weapon.Recoil
{
    [Serializable, AddTypeMenu("Random")]
    public class RandomRecoil : IRecoil
    {
        [SerializeField] private float duration;

        private float _verticalRecoil;
        private float _horizontalRecoil;
        private float _easeTime;

        public void Reset()
        {
            if (_easeTime > 0) return;
            var rotate = Locator<ICameraRotate>.Instance.Current;
            if (rotate == null) return;
            

            rotate.HorizontalOffset = Mathf.Lerp(rotate.HorizontalOffset, 0f, Time.deltaTime / duration);
            rotate.VerticalOffset = Mathf.Lerp(rotate.VerticalOffset, 0f, Time.deltaTime / duration);
        }

        public void Generate()
        {
            var random = Random.insideUnitCircle;
            _horizontalRecoil = random.y;
            _verticalRecoil = random.x;
            _easeTime = duration;
        }

        public void Easing()
        {
            var rotate = Locator<ICameraRotate>.Instance.Current;
            if (rotate == null) return;
            if (_easeTime < 0f) return;
            rotate.VerticalOffset += _verticalRecoil;
            rotate.HorizontalOffset *= _horizontalRecoil;
        }
    }
}