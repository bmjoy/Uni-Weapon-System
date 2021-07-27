using UnityEngine;

namespace WeaponSystem.Core.Input.OldInput
{
    [System.Serializable]
    public class UnityKeyboardWeaponInput : IWeaponInput
    {
        [SerializeField] private KeyCode[] attackKey = new[] {KeyCode.Mouse0};
        [SerializeField] private KeyCode[] altAttackKey = new[] {KeyCode.Mouse1};
        [SerializeField] private KeyCode[] reloadKey = new[] {KeyCode.R};
        [SerializeField] private KeyCode[] primaryAltAltActionKey = new[] {KeyCode.LeftShift};
        [SerializeField] private KeyCode[] secondaryAltActionKey = new[] {KeyCode.B};
        [SerializeField] private KeyCode[] lookWeaponKey = new[] {KeyCode.N};

        public bool IsPrimaryAction => attackKey.IsAnyKeyPressed();
        public bool IsPrimaryAltAltAction => primaryAltAltActionKey.IsAnyKeyPressed();
        public bool IsSecondaryAction => altAttackKey.IsAnyKeyPressed();
        public bool IsReload => reloadKey.IsAnyKeyPressed();
        public bool IsSecondaryAltAction => secondaryAltActionKey.IsAnyKeyPressed();
        public bool IsLookWeapon => lookWeaponKey.IsAnyKeyPressed();
    }
}