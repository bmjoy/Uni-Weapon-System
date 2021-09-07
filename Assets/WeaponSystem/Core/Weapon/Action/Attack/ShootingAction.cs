using System;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Core.Collision;
using WeaponSystem.Core.Movement;
using WeaponSystem.Core.Runtime.FireMode;
using WeaponSystem.Core.Runtime.Timer;
using WeaponSystem.Core.Weapon.Bullet;
using WeaponSystem.Core.Weapon.Magazine;
using WeaponSystem.Core.Weapon.Muzzle;
using WeaponSystem.Core.Weapon.Recoil;

namespace WeaponSystem.Core.Weapon.Action.Attack
{
    [Serializable, AddTypeMenu("Attack/Shooting")]
    public class ShootingAction : IWeaponAction
    {
        [SerializeReference, SubclassSelector] private IRpmTimer _rpm = new FixedRpmTimer();
        [SerializeReference, SubclassSelector] private IFireMode _fireMode = new FullAuto();
        [SerializeField] private uint useAmmoAmount = 1;
        [SerializeReference, SubclassSelector] private IMuzzle _muzzle = new SpreadMuzzle();
        [SerializeReference, SubclassSelector] private IRecoil _recoil = new NoneRecoil();
        [SerializeReference, SubclassSelector] private IBullet _bullet = new HitScanBullet();

        public UnityEvent onFire;
        public UnityEvent onAmmoEmpty;

        private IObjectPermission _permission;
        private IObjectGroup _group;
        private IMagazine _magazine;

        public void Injection(Transform parent, IMagazine magazine)
        {
            _permission = parent.GetComponent<IObjectPermission>();
            _group = parent.GetComponentInParent<IObjectGroup>();
            _magazine = magazine;
        }

        public void Action(bool isAction, ref bool isAim, IPlayerState state)
        {
            _rpm.Update();
            _recoil?.Easing();

            if (_magazine?.IsReloading ?? false) return;

            if (_rpm.IsValid == false) return;

            if (_fireMode.Evaluate(isAction) == false)
            {
                _recoil?.Reset();
                _rpm.Reset();
                return;
            }

            if (_magazine?.UseAmmo(useAmmoAmount) == false)
            {
                onAmmoEmpty.Invoke();
                _recoil?.Reset();
                return;
            }

            _rpm.Lap();
            _recoil?.Generate();
            _muzzle.Defuse(state, isAim);
            onFire.Invoke();
            _bullet?.Shot(_muzzle.Position, _muzzle.Direction, _permission, _group);
        }

        public void AltAction(bool isAltAction, IPlayerState state) { }

        public void OnHolster(ref bool isAim) {}


        public void OnDraw(ref bool isAim) {}
        
    }
}