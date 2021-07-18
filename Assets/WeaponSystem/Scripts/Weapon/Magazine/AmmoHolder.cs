﻿using System;
using UnityEngine;
using WeaponSystem.Weapon.Muzzle;

namespace WeaponSystem.Scripts.Weapon.Magazine
{
    [Serializable]
    public class AmmoHolder : IAmmoHolder
    {
        [SerializeField] private int remaining = 120;

        public int Remaining
        {
            get => remaining;
            set => remaining = value;
        }

        public int GetAmmo(int amount)
        {
            if (remaining < amount)
            {
                var result = remaining;
                remaining = 0;
                return result;
            }

            remaining -= amount;
            return amount;
        }
    }
}