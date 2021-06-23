using System;
using System.Collections;
using UnityEngine;

namespace WeaponSystem.Weapon.Magazine
{
    [Serializable, AddTypeMenu("None")]
    public class NoneMagazine : IMagazine
    {
        private const int MaxValue = 999;
        public int Current => MaxValue;
        public bool UseAmmo(int useAmount) => true;
        public bool IsReloading => false;

        public IEnumerator Reload()
        {
            yield break;
        } 
    }
}