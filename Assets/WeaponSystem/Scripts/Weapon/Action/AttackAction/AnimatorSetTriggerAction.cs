using System;
using UnityEngine;
using WeaponSystem.Movement;
using WeaponSystem.Scripts.Runtime;
using WeaponSystem.Weapon.Action.AltAttackAction;
using WeaponSystem.Weapon.Action.Utils;
using WeaponSystem.Weapon.Magazine;
using WeaponSystem.Weapon.Recoil;

namespace WeaponSystem.Weapon.Action.AttackAction
{
    /// <summary>
    /// 近接武器などのアニメーションのみのクラス
    /// </summary>
    [Serializable, AddTypeMenu("Attack/AnimatorSetTrigger")]
    public class AnimatorSetTriggerAction : IAttackAction, IAltAttackAction
    {
        [SerializeReference, SubclassSelector] private IRpmTimer _rpm = new FixedRpmTimer();
        [SerializeField] private int useAmmoAmount = 1;
        [SerializeField] private string actionName = "AttackAction";
        [SubclassSelector] private IFireMode _fireMode = new SemiAuto();
        [SubclassSelector] private IRecoil _recoil = new NoneRecoil();

        private IMagazine _magazine;
        private Animator _animator;

        public void Injection(Transform parent, Animator animator, IMagazine magazine)
        {
            _magazine = magazine;
            _animator = animator;
        }

        public void Action(bool isAction, IPlayerContext context)
        {
            _rpm.Update();
            _recoil.Easing();
            if (_fireMode.Evaluate(isAction) == false)
            {
                _recoil.Reset();
                return;
            }

            if (_rpm.IsValid == false) return;
            if (_magazine.UseAmmo(useAmmoAmount) == false) return;
            _animator.NullCast()?.SetTrigger(actionName);
        }
    }
}