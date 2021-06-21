using System.Collections;
using WeaponSystem.Weapon.Action;

namespace WeaponSystem.Weapon.Magazine
{
    public interface IMagazine : IWeaponAction
    {
        public int Current { get; }
        public bool UseAmmo(int useAmount);
        public bool IsReloading { get; }
        public IEnumerator Reload();
    }
}