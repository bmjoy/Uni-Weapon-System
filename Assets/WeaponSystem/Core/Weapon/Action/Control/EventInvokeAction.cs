using System;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Core.Movement;
using WeaponSystem.Core.Weapon.Magazine;

namespace WeaponSystem.Core.Weapon.Action.Control
{
    [Serializable, AddTypeMenu("Control/EventEventInvoke")]
    public class EventInvokeAction : IWeaponAction
    {
        public UnityEvent<bool> onAction;
        public UnityEvent<bool> onAltAction;

        public void Injection(Transform parent, IMagazine magazine) { }
        public void Action(bool isAction,ref bool isAim, IPlayerState state) => onAction.Invoke(isAction);

        public void AltAction(bool isAltAction, IPlayerState state) => onAltAction.Invoke(isAltAction);

        public void OnHolster(ref bool isAim) {}


        public void OnDraw(ref bool isAim) {}
    }
}