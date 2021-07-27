using System;
using UnityEngine;
using WeaponSystem.Core.Movement;
using WeaponSystem.Core.Weapon.Magazine;

namespace WeaponSystem.Core.Weapon.Action
{
    [Serializable, AddTypeMenu("None")]
    public class NoneAction : IWeaponAction
    {
        public void Injection(Transform parent, IMagazine magazine) { }
        public void Action(bool isAction, IPlayerContext context) { }
        public void AltAction(bool isAltAction, IPlayerContext context) { }
    }
}