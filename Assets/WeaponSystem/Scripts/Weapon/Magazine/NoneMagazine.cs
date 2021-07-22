using System;
using System.Collections;
using UnityEngine;
using WeaponSystem.Weapon.Muzzle;

namespace WeaponSystem.Weapon.Magazine
{
    [Serializable, AddTypeMenu("None")]
    public class NoneMagazine : IMagazine
    {
        public void Injection(Animator animator) { }

        public IAmmoHolder AmmoHolder { get; set; }
        public int Current => AmmoHolder.Remaining;
        public bool UseAmmo(int useAmount) => AmmoHolder.GetAmmo(useAmount) > 0;
        public bool IsReloading => false;

        public IEnumerator Reload()
        {
            yield break;
        }
    }
}