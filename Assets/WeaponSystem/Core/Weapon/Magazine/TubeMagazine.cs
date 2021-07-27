using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Core.Weapon.AmmoHolder;

namespace WeaponSystem.Core.Weapon.Magazine
{
    [Serializable, AddTypeMenu("Tube")]
    public class TubeMagazine : IMagazine
    {
        [SerializeField] private float reloadTime = .1f;
        [SerializeField] private uint reloadAmount = 1;
        [SerializeField] private uint maxAmount = 8;

        public UnityEvent onReload;
        public UnityEvent onReloadStart;
        public UnityEvent onReloadEnd;
        
        private WaitForSeconds _reload;

        public void Injection(Animator animator) { }

        public IAmmoHolder AmmoHolder { get; set; }
        public uint Reaming { get; private set; }

        public bool UseAmmo(uint useAmount)
        {
            if (useAmount > Reaming) return false;
            Reaming -= useAmount;
            return true;
        }

        public bool IsReloading { get; private set; }

        public IEnumerator Reload()
        {
            IsReloading = true;
            while (Reaming <= maxAmount && AmmoHolder.Remaining > 0)
            {
                onReload.Invoke();
                yield return _reload ??= new WaitForSeconds(reloadTime);
                Reaming += AmmoHolder.GetAmmo(reloadAmount);
                UnityEngine.Debug.Log($"Amount: {Reaming.ToString()}");
            }

            IsReloading = false;
        }
    }
}