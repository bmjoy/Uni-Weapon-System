namespace WeaponSystem.Core.Input
{
    public interface IWeaponInput
    {
        bool IsPrimaryAction { get; }
        bool IsPrimaryAltAltAction { get; }
        bool IsSecondaryAction { get; }
        bool IsSecondaryAltAction { get; }
        bool IsReload { get; }
        bool IsLookWeapon { get; }
    }
}