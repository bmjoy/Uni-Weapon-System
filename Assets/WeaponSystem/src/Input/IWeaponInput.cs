namespace WeaponSystem.Input
{
    public interface IWeaponInput
    {
        bool IsAttack { get; }
        bool IsAltAttack { get; }
        bool IsReload { get; }
        bool IsModeChanged { get; }
        bool IsLookWeapon { get; }
    }
}