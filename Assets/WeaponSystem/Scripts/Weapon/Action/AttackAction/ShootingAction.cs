using System;
using UnityEngine;
using WeaponSystem.Collision;
using WeaponSystem.Effect;
using WeaponSystem.Movement;
using WeaponSystem.Scripts.Runtime;
using WeaponSystem.Weapon.Action;
using WeaponSystem.Weapon.Action.AltAttackAction;
using WeaponSystem.Weapon.Action.AttackAction;
using WeaponSystem.Weapon.Bullet;
using WeaponSystem.Weapon.Magazine;
using WeaponSystem.Weapon.Muzzle;
using WeaponSystem.Weapon.Recoil;

namespace WeaponSystem
{
    [Serializable, AddTypeMenu("Attack/Shooting")]
    public class ShootingAction : IAttackAction, IAltAttackAction
    {
        [SerializeReference, SubclassSelector] private IRpmTimer _rpm = new Weapon.Action.Utils.FixedRpmTimer();
        [SerializeReference, SubclassSelector] private IFireMode _fireMode = new FullAuto();
        [SerializeField] private int useAmmoAmount = 1;
        [SerializeReference, SubclassSelector] private IMuzzle _muzzle = new DefusingMuzzle();
        [SerializeReference, SubclassSelector] private IRecoil _recoil = new NoneRecoil();
        [SerializeReference, SubclassSelector] private IBullet _bullet = new HitScanBullet();
        [SerializeField] private string shootingAnimParamName = "Shot";
        [SerializeReference, SubclassSelector] private IEffect _muzzleFlash = new NoneEffect();

        private IObjectPermission _permission;
        private IObjectGroup _group;
        private Animator _animator;
        private IMagazine _magazine;
        private int? _animationTriggerHash;
        private Transform _muzzleFlashRoot;

        public void Injection(Transform parent, Animator animator, IMagazine magazine)
        {
            _permission = parent.GetComponent<IObjectPermission>();
            _group = parent.GetComponentInParent<IObjectGroup>();
            _animator = animator;
            _magazine = magazine;

            _muzzleFlashRoot = new GameObject("Muzzle Flash pool").transform;
        }

        void IAttackAction.Action(bool isAction, IPlayerContext context) => ShotAction(isAction, context);

        void IAltAttackAction.Action(bool isAction, IPlayerContext context) => ShotAction(isAction, context);

        private void ShotAction(bool isAction, IPlayerContext context)
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
                _recoil?.Reset();
                return;
            }

            if (shootingAnimParamName != string.Empty)
            {
                _animationTriggerHash ??= Animator.StringToHash(shootingAnimParamName);
                _animator.NullCast()?.SetBool(_animationTriggerHash!.Value, false);
            }

            _rpm.Lap();
            _recoil?.Generate();
            _muzzle.Defuse(context);

            _muzzleFlash?.Play(_muzzle.Position, Quaternion.identity, _muzzleFlashRoot);
            
            if (shootingAnimParamName != string.Empty)
            {
                _animationTriggerHash ??= Animator.StringToHash(shootingAnimParamName);
                _animator.NullCast()?.SetBool(_animationTriggerHash!.Value, true);
            }
            _bullet?.Shot(_muzzle.Position, _muzzle.Direction, _permission, _group);
        }
    }
}