using System.Collections;
using UnityEngine.Events;
using WeaponSystem.Core.Weapon.AmmoHolder;

namespace WeaponSystem.Core.Weapon.Magazine
{
    public abstract class MagazineBase : IMagazine
    {
        public UnityEvent<uint> onReamingChange;
        
        public abstract IAmmoHolder AmmoHolder { get; set; }
        public abstract uint Capacity { get; }
        public abstract uint Reaming { get; }
        public abstract bool UseAmmo(uint useAmount);
        public abstract bool IsReloading { get; }
        public abstract IEnumerator Reload();
    }
}