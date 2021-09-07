using WeaponSystem.Core.Weapon.AmmoHolder;
using WeaponSystem.Core.Weapon.Magazine;


namespace WeaponSystem.Core.Weapon
{
    public interface IWeapon
    {
        public bool IsAim { get; }
        public IMagazine Magazine { get; }
        public IAmmoHolder AmmoHolder { get; }
    }
}