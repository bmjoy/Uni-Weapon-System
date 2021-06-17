﻿using System;
using UnityEngine;
using WeaponSystem.Movement;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action.AltAttackAction
{
    [Serializable, AddTypeMenu("None")]
    public class NoneAltAttackAction : IAltAttackAction
    {
        // 何もしないよ!
        public void Injection(Transform parent, Animator animator, IMagazine magazine) { }
        public void Action(bool isAction, IPlayerContext context) { }
    }
}