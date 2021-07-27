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
        public void Action(bool isAction, IPlayerContext context) => onAction.Invoke(isAction);

        public void AltAction(bool isAltAction, IPlayerContext context) => onAltAction.Invoke(isAltAction);
    }
}