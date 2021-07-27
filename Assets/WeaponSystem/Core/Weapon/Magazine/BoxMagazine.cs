using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Core.Weapon.AmmoHolder;

namespace WeaponSystem.Core.Weapon.Magazine
{
    [Serializable, AddTypeMenu("Box")]
    public class BoxMagazine : IMagazine
    {
        [SerializeField] private float reloadTime = .5f;
        [SerializeField] private float tacticalReloadTime = .3f;
        [SerializeField] private uint maxAmount = 30;
        [SerializeField, ReadOnly] private uint current;

        public UnityEvent onTacticalReloadStart;
        public UnityEvent onTacticalReloadEnd;
        public UnityEvent onEmptyReloadStart;
        public UnityEvent onEmptyReloadEnd;

        private WaitForSeconds _tacticalReload;
        private WaitForSeconds _reload;

        public IAmmoHolder AmmoHolder { get; set; }

        public uint Reaming => current;

        public bool UseAmmo(uint useAmount = 1)
        {
            useAmount = (uint) Mathf.Clamp(useAmount, 0, Int32.MaxValue);
            current = (uint) Mathf.Clamp(current, 0, maxAmount);
            if (useAmount > Reaming) return false;
            current -= useAmount;
            return true;
        }

        public bool IsReloading => _isReloading;
        private bool _isReloading;

        public IEnumerator Reload()
        {
            current = (uint) Mathf.Clamp(current, 0, maxAmount);
            var reloadAmount = maxAmount - current;
            if (AmmoHolder.IsEmpty) yield break;

            _isReloading = true;

            if (Reaming > 0)
            {
                onTacticalReloadStart.Invoke();
                yield return _tacticalReload ??= new WaitForSeconds(tacticalReloadTime);
                onTacticalReloadEnd.Invoke();
                current += AmmoHolder.GetAmmo(reloadAmount) + 1;
            }
            else
            {
                onEmptyReloadStart.Invoke();
                yield return _reload ??= new WaitForSeconds(reloadTime);
                current += AmmoHolder.GetAmmo(reloadAmount);
                onEmptyReloadEnd.Invoke();
            }

            _isReloading = false;
        }
    }
}