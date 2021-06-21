using System;
using System.Collections;
using UnityEngine;

namespace WeaponSystem.Weapon.Magazine
{
    [Serializable]
    public class TubeMagazine : IMagazine
    {
        [SerializeField] private float reloadTime;
        [SerializeField] private int reloadAmount = 1;
        [SerializeField] private int maxAmount = 8;

        private WaitForSeconds _reload;

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

            while (Current <= maxAmount)
            {
                yield return _reload;
                Current += reloadAmount;
                Debug.Log($"Amount: {Current.ToString()}");
            }

            IsReloading = false;
        }

        public void Injection(Transform parent, Animator animator, IMagazine magazine) { }
    }
}