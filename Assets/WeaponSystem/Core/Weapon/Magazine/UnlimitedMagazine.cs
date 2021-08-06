using System;
using System.Collections;
using WeaponSystem.Core.Weapon.AmmoHolder;

namespace WeaponSystem.Core.Weapon.Magazine
{
    [Serializable, AddTypeMenu("Unlimited")]
    public class UnlimitedMagazine : IMagazine
    {
        public IAmmoHolder AmmoHolder { get; set; }
        public uint Capacity => uint.MaxValue;
        public uint Reaming => AmmoHolder.Remaining;
        public bool UseAmmo(uint useAmount) => AmmoHolder.GetAmmo(useAmount) > 0;
        public bool IsReloading => false;

        public IEnumerator Reload()
        {
            yield break;
        }
    }
}