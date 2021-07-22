using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;
using WeaponSystem.Scripts.Debug;
using WeaponSystem.Weapon.Muzzle;

namespace WeaponSystem.Weapon.Magazine
{
    [Serializable, AddTypeMenu("Box")]
    public class BoxMagazine : IMagazine
    {
        [SerializeField] private float reloadTime = .5f;
        [SerializeField] private float tacticalReloadTime = .3f;
        [SerializeField] private int maxAmount = 30;
        [SerializeField, ReadOnly] private int current;
        [SerializeField] private string reloadAnimationParam = "isReload";
        [SerializeField] private string tacticalReloadAnimationParam = "isTacticalReload";
        
        private WaitForSeconds _tacticalReload;
        private WaitForSeconds _reload;
        private Animator _animator;

        public void Injection(Animator animator) => _animator = animator;

        public IAmmoHolder AmmoHolder { get; set; }
        
        public int Current => current;

        public bool UseAmmo(int useAmount = 1)
        {
            useAmount = Mathf.Clamp(useAmount, 0, Int32.MaxValue);
            current = Mathf.Clamp(current, 0, maxAmount);
            if (useAmount > Current) return false;
            current -= useAmount;
            return true;
        }

        public bool IsReloading => _isReloading;
        private bool _isReloading;

        public IEnumerator Reload()
        {
            current = Mathf.Clamp(current, 0, maxAmount);
            var reloadAmount = maxAmount - current;
            if (AmmoHolder.IsEmpty) yield break;
            _isReloading = true;

            if (Current > 0)
            {
                yield return _tacticalReload ??= new WaitForSeconds(tacticalReloadTime);
                _animator.SetBool(tacticalReloadAnimationParam, IsReloading);
                Debug.Log((maxAmount - current).ToString());
                current += AmmoHolder.GetAmmo(reloadAmount )+ 1;
            }
            else
            {
                yield return _reload ??= new WaitForSeconds(reloadTime);
                _animator.SetBool(reloadAnimationParam, IsReloading);
                current += AmmoHolder.GetAmmo(reloadAmount);
            }

            _isReloading = false;
        }
    }
}