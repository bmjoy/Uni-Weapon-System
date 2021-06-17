using System;
using Audio;
using UnityEngine;
using WeaponSystem.Movement;
using WeaponSystem.Scripts.Runtime;
using WeaponSystem.Weapon.Action;
using WeaponSystem.Weapon.Action.AltAttackAction;
using WeaponSystem.Weapon.Action.AttackAction;
using WeaponSystem.Weapon.Action.Utils;
using WeaponSystem.Weapon.Bullet;
using WeaponSystem.Weapon.Magazine;
using WeaponSystem.Weapon.Muzzle;
using WeaponSystem.Weapon.Recoil;
using Random = UnityEngine.Random;

namespace WeaponSystem
{
    [Serializable, AddTypeMenu("Attack/Shooting")]
    public class ShootingAction : IAttackAction, IAltAttackAction
    {
        [SerializeReference, SubclassSelector] private IRpmTimer _rpm = new FixedRpmTimer();
        [SerializeReference, SubclassSelector] private IFireMode _fireMode = new FullAuto();
        [SerializeField] private int useAmmoAmount = 1;
        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeReference, SubclassSelector] private IMuzzle _muzzle = new DefusingMuzzle();
        [SerializeReference, SubclassSelector] private IRecoil _recoil = new SinRandomRecoil();
        [SerializeReference, SubclassSelector] private IBullet _bullet = new HitScanBullet();

        [SerializeField] private string shotActionSoundName = "Shot";

        private ISoundPlayer _soundPlayer;
        private Animator _animator;
        private static readonly int Shot = Animator.StringToHash("Shot");
        private IMagazine _magazine;

        public void Injection(Transform parent, Animator animator, IMagazine magazine)
        {
            _soundPlayer = parent.GetComponent<ISoundPlayer>();
            _animator = animator;
            _magazine = magazine;
        }

        void IAttackAction.Action(bool isAction, IPlayerContext context) => ShotAction(isAction, context);

        void IAltAttackAction.Action(bool isAction, IPlayerContext context) => ShotAction(isAction, context);

        private void ShotAction(bool isAction, IPlayerContext context)
        {
            _rpm.Update();
            _recoil.Easing();

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

            _soundPlayer?.Play(shotActionSoundName);
            if (_soundPlayer != null) _soundPlayer.Pitch = Random.Range(.95f, 1f);
            _rpm.Lap();
            _recoil?.Generate();
            _muzzle.Defuse(context);


            _animator.NullCast()?.SetTrigger(Shot);

            _bullet?.Shot(_muzzle.Position, _muzzle.Direction);
        }
    }
}