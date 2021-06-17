using UnityEngine;

namespace WeaponSystem
{
    public static class FovSettings
    {
        public static float BaseFov
        {
            get => _baseFov;
            set => _baseFov = Mathf.Clamp(value, 10f, 100f);
        }

        private static float _baseFov = 60f;
    }
}