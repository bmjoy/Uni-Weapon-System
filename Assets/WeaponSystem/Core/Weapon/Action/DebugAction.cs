﻿using System;
using UnityEngine;
using WeaponSystem.Core.Debug;
using WeaponSystem.Core.Movement;
using WeaponSystem.Core.Weapon.Magazine;

namespace WeaponSystem.Core.Weapon.Action
{
    [Serializable, AddTypeMenu("Debug")]
    public class DebugAction : IWeaponAction
    {
        public void Injection(Transform parent, IMagazine magazine)
        {
            parent.name.Log();
        }

        public void Action(bool isAction, IPlayerContext context) => $"isAction: {isAction.ToString()}".Log();

        public void AltAction(bool isAltAction, IPlayerContext context) => $"isAltAction: {isAltAction.ToString()}".Log();
    }
}