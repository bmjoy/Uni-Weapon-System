using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;

namespace WeaponSystem.Weapon.Magazine
{
    [Serializable, AddTypeMenu("Box")]
    public class BoxMagazine : IMagazine
    {
        [SerializeField] private float reloadTime = .5f;
        [SerializeField] private float tacticalReloadTime = .3f;
        [SerializeField] private int maxAmount = 30;
        [SerializeField, ReadOnly] private int current;

        private WaitForSeconds _tacticalReload;
        private WaitForSeconds _reload;

        public int Current => current;

        public bool UseAmmo(int useAmount = 1)
        {
            useAmount = Mathf.Clamp(useAmount, 0, Int32.MaxValue);
            if (useAmount > Current) return false;
            current -= useAmount;
            return true;
        }

        public bool IsReloading { get; private set; }

        public IEnumerator Reload()
        {
            IsReloading = true;

            if (Current > 0)
            {
                yield return _tacticalReload ??= new WaitForSeconds(tacticalReloadTime);
                current = maxAmount + 1;
            }
            else
            {
                yield return _reload ??= new WaitForSeconds(reloadTime);
                current = maxAmount;
            }

            IsReloading = false;
        } 
    }
}