using System;
using UnityEngine;
using AudioSystem;
using WeaponSystem.Movement;
using WeaponSystem.Scripts.Runtime;
using WeaponSystem.Weapon.Action.AltAttackAction;
using WeaponSystem.Weapon.Action.Utils;
using WeaponSystem.Weapon.Bullet;
using WeaponSystem.Weapon.Magazine;
using WeaponSystem.Weapon.Muzzle;
using WeaponSystem.Weapon.Recoil;
using Random = UnityEngine.Random;

namespace WeaponSystem.Weapon.Action.AttackAction
{
    [Serializable, AddTypeMenu("Attack/Shooting")]
    public class ShootingAction : IAttackAction, IAltAttackAction
    {
        [SerializeField] private RPMTimer rpm = new RPMTimer(600f);
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

        public void Injection(Transform parent, Animator animator, IMagazine magazine, IPlayerContext context)
        {
            _soundPlayer = parent.GetComponent<ISoundPlayer>();
            _animator = animator;
            _magazine = magazine;
        }

        void IAttackAction.Action(bool isAction) => ShotAction(isAction);

        void IAltAttackAction.Action(bool isAction) => ShotAction(isAction);

        private void ShotAction(bool isAction)
        {
            rpm.Update();
            _recoil.Easing();

            if (_magazine?.IsReloading ?? false) return;

            if (rpm.IsValid == false) return;

            if (_fireMode.Evaluate(isAction) == false)
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
            if (_soundPlayer != null) _soundPlayer.Pitch = Random.Range(.9f, 1f);
            rpm.CountReset();
            _recoil?.Generate();
            _muzzle.Defuse();


            _animator.NullCast()?.SetTrigger(Shot);

            _bullet?.Shot(_muzzle.Position, _muzzle.Direction);
        }
    }
}