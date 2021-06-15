using System.Collections;
using UnityEngine;
using WeaponSystem.Scripts;

namespace WeaponSystem.Weapon.Magazine
{
    [System.Serializable]
    public class NoneMagazine : IMagazine
    {
        private const int MaxValue = 999; 
        public int Current => MaxValue;
        public bool UseAmmo(int useAmount) => true;
        public bool IsReloading => false;
        public IEnumerator Reload() { yield break; }
    }
}