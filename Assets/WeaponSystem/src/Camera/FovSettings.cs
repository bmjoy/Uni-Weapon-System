using UnityEngine;

namespace WeaponSystem
{
    public static class FovSettings
    {
        public static float BaseFov
        {
            get => _baseFov;
            set => _baseFov = Mathf.Abs(value);
        }

        private static float _baseFov = 60f;
    }
}