using System;
using UnityEngine;
using WeaponSystem.Collision;
using WeaponSystem.Effect;
using WeaponSystem.Movement;
using WeaponSystem.Scripts.Runtime;
using WeaponSystem.Weapon.Action.AltAttackAction;
using WeaponSystem.Weapon.Action.Utils;
using WeaponSystem.Weapon.Bullet;
using WeaponSystem.Weapon.Magazine;
using WeaponSystem.Weapon.Muzzle;
using WeaponSystem.Weapon.Recoil;
using WeaponSystem.Weapon.ShotgunPattern;

namespace WeaponSystem.Weapon.Action.AttackAction
{
    [Serializable, AddTypeMenu("Attack/ShotgunShooting")]
    public class ShotgunShootingAction : IAttackAction, IAltAttackAction
    {
        [SerializeReference] private IRpmTimer _rpm = new FixedRpmTimer();
        [SerializeReference, SubclassSelector] private IFireMode _mode = new FullAuto();
        [SerializeField] private int useAmmoAmount = 1;
        [SerializeReference, SubclassSelector] private IMuzzle _muzzle = new DefusingMuzzle();
        [SerializeReference, SubclassSelector] private IRecoil _recoil = new SinRandomRecoil();
        [SerializeReference, SubclassSelector] private IBullet _bullet = new HitScanBullet();
        [SerializeReference, SubclassSelector] private IShotgunDefuse _shotgunDefuse = new RandomShotgunDefuse();
        [SerializeReference, SubclassSelector] private IEffect _muzzleFlash = new NoneEffect();
        [SerializeField] private string animationTriggerName;
        
        private IObjectPermission _permission;
        private IObjectGroup _group;
        private IMagazine _magazine;
        private Animator _animator;
        private int _animationTriggerHash;
        
        public void Injection(Transform parent, Animator animator, IMagazine magazine)
        {
            _permission = parent.GetComponent<IObjectPermission>();
            _group = parent.GetComponentInParent<IObjectGroup>();
            _magazine = magazine;
            _animator = animator;
            _animationTriggerHash = Animator.StringToHash(animationTriggerName);
        }

        void IAttackAction.Action(bool isAction, IPlayerContext context) => ShotAction(isAction, context);

        void IAltAttackAction.Action(bool isAction, IPlayerContext context) => ShotAction(isAction, context);


        private void ShotAction(bool isAction, IPlayerContext context)
        {
            _rpm.Update();
            _recoil?.Easing();

            if (_magazine?.IsReloading ?? false) return;

            if (_rpm.IsValid == false) return;
            if (_mode.Evaluate(isAction) == false)
            {
                _recoil?.Reset();
                return;
            }

            if (_magazine?.UseAmmo(useAmmoAmount) == false)
            {
                _recoil?.Reset();
                return;
            }

            _rpm.Lap();
            _recoil?.Generate();
            _muzzle.Defuse(context);
            
            _animator.NullCast()?.SetTrigger(_animationTriggerHash);
            _muzzleFlash?.Play(_muzzle.Position, Quaternion.LookRotation(_muzzle.Direction), null);

            foreach (var offset in _shotgunDefuse)
            {
                var muzzleOffset = _muzzle.Rotation * offset;
                _bullet.Shot(_muzzle.Position, _muzzle.Direction + muzzleOffset, _permission, _group);
            }
        }
    }
}