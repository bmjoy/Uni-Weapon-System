using UnityEngine;

namespace WeaponSystem.Weapon.Action.AttackAction
{
    [System.Serializable]
    public class RPMTimer
    {
        [SerializeField, Range(10f, 2000f)] private float rpm;

        private const float Minute = 60f;

        private float _counter;

        public RPMTimer(float rpm) => this.rpm = rpm;

        public bool IsValid => _counter > Minute / rpm;

        public void Update()
        {
            if (_counter > Minute / rpm) return;
            _counter += Time.deltaTime;
        }

        public void CountReset() => _counter = 0f;
    }
}