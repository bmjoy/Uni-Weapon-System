using System;
using System.Collections;
using UnityEngine;
using WeaponSystem.Core.Weapon.AmmoHolder;

namespace WeaponSystem.Core.Weapon.Magazine
{
    [Serializable, AddTypeMenu("None")]
    public class NoneMagazine : IMagazine
    {
        public void Injection(Animator animator) { }

        public IAmmoHolder AmmoHolder { get; set; }
        public uint Reaming => AmmoHolder.Remaining;
        public bool UseAmmo(uint useAmount) => AmmoHolder.GetAmmo(useAmount) > 0;
        public bool IsReloading => false;

        public IEnumerator Reload()
        {
            yield break;
        }
    }
}