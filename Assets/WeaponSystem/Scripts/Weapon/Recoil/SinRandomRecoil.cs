using UnityEngine;
using static UnityEngine.Mathf;
using WeaponSystem.Camera;
using WeaponSystem.Runtime;


namespace WeaponSystem.Weapon.Recoil
{
    [System.Serializable, AddTypeMenu("SinRandom")]
    public class SinRandomRecoil : IRecoil
    {
        [SerializeField] private float kickPower = 2f;
        [SerializeField] private float horizontalFrequency = 25f;
        [SerializeField] private float horizontalWidth = 2f;
        [SerializeField] private float rollBackTime = .15f;
        [SerializeField] private AnimationCurve horizontalWeightCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        [SerializeField] private AnimationCurve verticalWeightCurve = AnimationCurve.Constant(0f, 1f, 1f);
        [SerializeField] private float easingDuration = .1f;

        private bool _isFire;
        private float _easeTime;
        private float _hRecoil;
        private float _vRecoil;

        public void Reset()
        {
            if (_isFire) return;
            var rotate = Locator<ICameraRotate>.Instance.Current;
            var spd = Vector3.zero;
            Vector3 current = new Vector3(rotate.VerticalOffset, rotate.HorizontalOffset);
            var damp = Vector3.SmoothDamp(current, Vector3.zero, ref spd, Time.deltaTime / rollBackTime);
            rotate.VerticalOffset = damp.x;
            rotate.HorizontalOffset = damp.y;
        }

        public void Generate()
        {
            _easeTime = easingDuration;
            _hRecoil = GenerateHorizontalRecoil();
            _vRecoil = GenerateVerticalRecoil();
            kickPower = Abs(kickPower);
        }

        public void Easing()
        {
            var rotate = Locator<ICameraRotate>.Instance.Current;

            if (rotate == null) return;

            if (_easeTime < 0f)
            {
                _isFire = false;
                return;
            }

            rotate.HorizontalOffset += _hRecoil * Time.deltaTime / easingDuration;
            rotate.VerticalOffset += _vRecoil * Time.deltaTime / easingDuration;
            _easeTime -= Time.deltaTime;
        }

        private float GenerateHorizontalRecoil()
        {
            var horizontal = Sin(Time.time * (PI * horizontalFrequency));
            horizontal *= horizontalWeightCurve.Evaluate(Random.value);
            return horizontal * kickPower * horizontalWidth;
        }

        private float GenerateVerticalRecoil()
        {
            var vertical = verticalWeightCurve.Evaluate(Random.value);
            return vertical * kickPower;
        }
    }
}