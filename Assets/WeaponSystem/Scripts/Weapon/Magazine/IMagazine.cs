using System.Collections;
using UnityEngine;
using WeaponSystem.Weapon.Muzzle;

namespace WeaponSystem.Weapon.Magazine
{
    public interface IMagazine
    {
        public void Injection(Animator animator);
        
        public IAmmoHolder AmmoHolder { get; set; }
        public int Current { get; }
        public bool UseAmmo(int useAmount);
        public bool IsReloading { get; }
        public IEnumerator Reload();
    }
}