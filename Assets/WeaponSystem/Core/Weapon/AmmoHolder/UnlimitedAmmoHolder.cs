using System;

namespace WeaponSystem.Core.Weapon.AmmoHolder
{
    [Serializable, AddTypeMenu("Unlimited")]
    public class UnlimitedAmmoHolder : IAmmoHolder
    {
        public bool IsEmpty => false;
        public uint Remaining { get; set; }

        public uint GetAmmo(uint amount) => amount;
    }
}