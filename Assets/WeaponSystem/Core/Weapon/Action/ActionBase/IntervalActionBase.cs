using UnityEngine;
using WeaponSystem.Core.Movement;
using WeaponSystem.Core.Runtime.FireMode;
using WeaponSystem.Core.Runtime.Timer;
using WeaponSystem.Core.Weapon.Magazine;


namespace WeaponSystem.Core.Weapon.Action.ActionBase
{
    public abstract class IntervalActionBase : IWeaponAction
    {
        [SerializeReference, SubclassSelector] private IRpmTimer _rpmTimer = new FixedRpmTimer();
        [SerializeReference, SubclassSelector] private IFireMode _fireMode;

        public virtual void Injection(Transform parent, IMagazine magazine) { }


        public void Action(bool isAction, ref bool isAim, IPlayerState state)
        {
            _rpmTimer.Update();
            BeforeInterval();

            if (_rpmTimer.IsValid == false) return;
            if (_fireMode.Evaluate(isAction) == false) return;

            AfterInterval();
        }


        protected abstract void BeforeInterval();

        protected abstract void AfterInterval();

        public virtual void AltAction(bool isAltAction, IPlayerState state) { }

        public virtual void OnHolster(ref bool isAim){}


        public virtual void OnDraw(ref bool isAim){}
    }
}