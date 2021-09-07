using UnityEngine;
using WeaponSystem.Core.Movement;
using WeaponSystem.Core.Weapon.Magazine;


namespace WeaponSystem.Core.Weapon.Action
{
    public interface IWeaponAction
    {
        /// <summary>
        /// 依存性注入をするためのメソッドです
        /// 独自にWeaponActionクラスを作成するときはAwake()でコールしてください
        /// </summary>
        void Injection(Transform parent, IMagazine magazine);


        /// <summary>
        /// 動作の核になるアクションです
        /// 希望する動作を実装してください
        /// </summary>
        /// <param name="isAction"></param>
        /// <param name="isAim"></param>
        /// <param name="state"></param>
        void Action(bool isAction, ref bool isAim, IPlayerState state);


        /// <summary>
        /// オプションを設定するアクションです。
        /// </summary>
        /// <param name="isAltAction"></param>
        /// <param name="state"></param>
        void AltAction(bool isAltAction, IPlayerState state);
        
        void OnHolster(ref bool isAim);

        void OnDraw(ref bool isAim);
    }
}