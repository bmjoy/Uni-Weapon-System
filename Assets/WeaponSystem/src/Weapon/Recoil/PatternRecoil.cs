using UnityEngine;
using static UnityEngine.Mathf;
using WeaponSystem.Camera;
using WeaponSystem.Runtime;


namespace WeaponSystem.Weapon.Recoil
{
    [System.Serializable]
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

            rotate.Horizontal += (patternData[_index].x * (1f / distance) * Time.deltaTime) / easingDuration;
            rotate.Vertical += (patternData[_index].y * (1f / distance) * Time.deltaTime) / easingDuration;
            _time -= Time.deltaTime;
        }
    }
}