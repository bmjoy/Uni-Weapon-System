namespace WeaponSystem.Core.Weapon
{
    public interface ISingleActionWeaponInput
    {
        public bool IsAction { get; }
        public bool IsAltAction { get; }
        public bool IsReload { get; }
    }
}