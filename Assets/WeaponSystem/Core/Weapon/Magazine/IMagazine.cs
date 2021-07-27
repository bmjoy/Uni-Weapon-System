using System.Collections;
using WeaponSystem.Core.Weapon.AmmoHolder;

namespace WeaponSystem.Core.Weapon.Magazine
{
    public interface IMagazine
    {
        public IAmmoHolder AmmoHolder { get; set; }
        public uint Reaming { get; }
        public bool UseAmmo(uint useAmount);
        public bool IsReloading { get; }
        public IEnumerator Reload();
    }
}