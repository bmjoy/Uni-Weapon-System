﻿using UnityEngine;
using WeaponSystem.Core.Movement;
using WeaponSystem.Core.Weapon.Magazine;

namespace WeaponSystem.Core.Weapon.Action
{
    public interface IWeaponAction
    {
        /// <summary>
        /// 依存性注入をするためのメソッドです
        /// 独自にWeaponクラスを作成するときはAwake(), Start(), OnEnable()でコールしてください
        /// </summary>
        void Injection(Transform parent, IMagazine magazine);

        void Action(bool isAction, ref bool isAim, IPlayerState state);

        void AltAction(bool isAltAction, IPlayerState state);
    }
}