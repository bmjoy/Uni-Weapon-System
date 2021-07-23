using UnityEngine;
using static UnityEngine.Input;

namespace WeaponSystem.Input
{
    public static class InputExtension
    {
        public static bool IsKeyPressed(this KeyCode keyCode) => GetKey(keyCode);
        public static bool IsKeyDown(this KeyCode keyCode) => GetKeyDown(keyCode);
        public static bool IsKeyUp(this KeyCode keyCode) => GetKeyUp(keyCode);

        public static bool IsAnyKeyPressed(this KeyCode[] keyCodes)
        {
            foreach (var keyCode in keyCodes)
            {
                if (keyCode.IsKeyPressed()) return true;
            }

            return false;
        }

        public static bool IsAnyKeyDown(this KeyCode[] keyCodes)
        {
            foreach (var keyCode in keyCodes)
            {
                if (keyCode.IsKeyDown()) return true;
            }

            return false;
        }

        public static bool IsAnyKeyUp(this KeyCode[] keyCodes)
        {
            foreach (var keyCode in keyCodes)
            {
                if (keyCode.IsKeyUp()) return true;
            }

            return false;
        }
    }
}