using System;
using UnityEngine;
using WeaponSystem.Camera;
using WeaponSystem.Runtime;
using static UnityEngine.Random;
using static UnityEngine.Mathf;

namespace WeaponSystem.Weapon.Recoil
{
    [Serializable, AddTypeMenu("Random")]
    public class RandomRecoil : IRecoil
    {
        [SerializeField] private float duration;
        [SerializeField] private float distance;
        [SerializeField] private AnimationCurve weightCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        private Vector2 _random;
        private float _time;

        public void Reset() { }

        public void Generate()
        {
            _time = duration;
            _random = insideUnitCircle;
        }

        public void Easing()
        {
            var rotate = Locator<ICameraRotate>.Instance.Current;
            if (_time < 0) return;
            _time -= Time.deltaTime;
            var horizontal = _random.x;
            var vertical = _random.y;
            horizontal *= weightCurve.Evaluate(Abs(horizontal));
            horizontal /= distance;
            horizontal /= duration;
            horizontal *= Time.deltaTime;
            
            vertical *= weightCurve.Evaluate(Abs(vertical));
            vertical /= distance;
            vertical /= duration;
            vertical *= Time.deltaTime;

        rotate.Horizontal += horizontal;
            rotate.Vertical += vertical;
        }
    }
}