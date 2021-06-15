using System;
using AudioSystem;
using UnityEngine;
using WeaponSystem.Movement;
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
        [SerializeField] private RPMTimer rpm = new RPMTimer(130f);
        [SerializeReference, SubclassSelector] private IFireMode _Mode = new FullAuto();
        [SerializeField] private int useAmmoAmount = 1;
        [SerializeReference, SubclassSelector] private IMuzzle _muzzle = new DefusingMuzzle();
        [SerializeReference, SubclassSelector] private IRecoil _recoil = new SinRandomRecoil();
        [SerializeReference, SubclassSelector] private IBullet _bullet = new HitScanBullet();
        [SerializeReference, SubclassSelector] private IShotgunDefuse _shotgunDefuse = new RandomShotgunDefuse();
        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeField] private string shotActionSoundName = "Shot";
        private ISoundPlayer _soundPlayer;
        private IMagazine _magazine;
        private Animator _animator;
        private static readonly int Shot = Animator.StringToHash("ShotAction");

        public void Injection(Transform parent, Animator animator, IMagazine magazine, IPlayerContext context)
        {
            _soundPlayer = parent.GetComponent<ISoundPlayer>();
            _magazine = magazine;
            _animator = animator;
        }

        void IAttackAction.Action(bool isAction) => ShotAction(isAction);

        void IAltAttackAction.Action(bool isAction) => ShotAction(isAction);


        private void ShotAction(bool isAction)
        {
            rpm.Update();
            _recoil?.Easing();

            if (_magazine?.IsReloading ?? false) return;

            if (rpm.IsValid == false) return;
            if (_Mode.Evaluate(isAction) == false)
            {
                _recoil?.Reset();
                return;
            }


            if (_magazine?.UseAmmo(useAmmoAmount) == false)
            {
                _recoil?.Reset();
                return;
            }

            _soundPlayer?.Play(shotActionSoundName);
            rpm.CountReset();
            _recoil?.Generate();
            
            _animator.SetTrigger(Shot);

            _muzzle.Defuse();
            
            foreach (var offset in _shotgunDefuse)
            {
                var muzzleOffset = _muzzle.Rotation * offset;
                _bullet.Shot(_muzzle.Position, _muzzle.Direction + muzzleOffset);
            }
        }
    }
}