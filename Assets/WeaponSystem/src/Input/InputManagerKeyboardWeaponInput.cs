using UnityEngine;

namespace WeaponSystem.Input
{
    [System.Serializable]
    public class InputManagerKeyboardWeaponInput : IWeaponInput
    {
        [SerializeField] private KeyCode[] attackKey = new KeyCode[] {KeyCode.Mouse0};

        [SerializeField] private KeyCode[] altAttackKey = new KeyCode[] {KeyCode.Mouse1};

        [SerializeField] private KeyCode[] reloadKey = new KeyCode[] {KeyCode.R};

        [SerializeField] private KeyCode[] modeChangeKey = new[] {KeyCode.B};
        [SerializeField] private KeyCode[] lookWeaponKey = new KeyCode[] {KeyCode.N};

        public bool IsAttack => attackKey.IsAnyKeyPressed();
        public bool IsAltAttack => altAttackKey.IsAnyKeyPressed();
        public bool IsReload => reloadKey.IsAnyKeyPressed();
        public bool IsModeChanged => modeChangeKey.IsAnyKeyDown();
        public bool IsLookWeapon => lookWeaponKey.IsAnyKeyPressed();
    }
}