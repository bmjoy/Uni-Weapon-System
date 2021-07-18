using System;
using WeaponSystem.Weapon.Muzzle;

namespace WeaponSystem.Scripts.Weapon.Magazine
{
    [Serializable]
    public class UnlimitedAmmoHolder : IAmmoHolder
    {
        public int Remaining { get; set; }

        public int GetAmmo(int amount) => amount;
    }
}