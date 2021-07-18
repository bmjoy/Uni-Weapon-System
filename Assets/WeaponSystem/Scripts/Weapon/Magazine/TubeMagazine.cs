using System;
using System.Collections;
using UnityEngine;
using WeaponSystem.Weapon.Muzzle;

namespace WeaponSystem.Weapon.Magazine
{
    [Serializable, AddTypeMenu("Tube")]
    public class TubeMagazine : IMagazine
    {
        [SerializeField] private float reloadTime;
        [SerializeField] private int reloadAmount = 1;
        [SerializeField] private int maxAmount = 8;

        private WaitForSeconds _reload;

        public void Injection(Animator animator) { }

        public IAmmoHolder AmmoHolder { get; set; }
        public int Current { get; private set; }

        public bool UseAmmo(int useAmount)
        {
            if (useAmount > Current) return false;
            Current -= useAmount;
            return true;
        }

        public bool IsReloading { get; private set; }

        public IEnumerator Reload()
        {
            IsReloading = true;

            _reload ??= new WaitForSeconds(reloadTime / maxAmount * reloadAmount);

            while (Current <= maxAmount && AmmoHolder.Remaining > 0)
            {
                yield return _reload;
                Current += AmmoHolder.GetAmmo(reloadAmount);
                Debug.Log($"Amount: {Current.ToString()}");
            }

            IsReloading = false;
        }
    }
}