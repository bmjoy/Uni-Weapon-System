﻿namespace WeaponSystem.Core.Weapon.AmmoHolder
{
    public interface IAmmoHolder
    {
        bool IsEmpty { get; }
        uint Remaining { get; set; }
        uint GetAmmo(uint amount);
    }
}