namespace WeaponSystem.Core.Weapon
{
    public interface IDualActionWeaponInput : ISingleActionWeaponInput
    {
        public bool IsSecondaryAction { get; }
        public bool IsSecondaryAltAction { get;  }
    }
}