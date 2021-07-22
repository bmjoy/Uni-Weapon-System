using System;
using WeaponSystem.Weapon.Muzzle;

namespace WeaponSystem.Scripts.Weapon.Magazine
{
    [Serializable, AddTypeMenu("Unlimited")]
    public class UnlimitedAmmoHolder : IAmmoHolder
    {
        public bool IsEmpty => false;
        public int Remaining { get; set; }

        public int GetAmmo(int amount) => amount;
    }
}