﻿using System;
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
        [SerializeField] private uint capacity = 8;
        [SerializeField] private uint reaming = 8;

        public UnityEvent<uint> onUseAmmo;
        
        public UnityEvent onReloadStart;
        public UnityEvent onReload;
        public UnityEvent onReloadEnd;

        private WaitForSeconds _reload;

        public IAmmoHolder AmmoHolder { get; set; }

        public uint Capacity => capacity;

        public uint Reaming
        {
            get => reaming;
            private set => reaming = value;
        }

        public bool UseAmmo(uint useAmount)
        {
            reaming = reloadAmount > capacity ? capacity : reaming;

            if (useAmount > Reaming) return false;
            Reaming -= useAmount;
            onUseAmmo.Invoke(Reaming);
            return true;
        }

        public bool IsReloading { get; private set; }

        public IEnumerator Reload()
        {
            if (reaming >= capacity) yield break;

            IsReloading = true;
            onReloadStart.Invoke();

            while (Reaming < Capacity)
            {
                var ammo = AmmoHolder.GetAmmo(1);
                if (ammo < 1) yield break;
                yield return _reload ??= new WaitForSeconds(reloadTime);
                Reaming++;
                onReload.Invoke();
            }

            onReloadEnd.Invoke();
            IsReloading = false;
        }
    }
}