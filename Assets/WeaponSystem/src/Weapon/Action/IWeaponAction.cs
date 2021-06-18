using UnityEngine;
using WeaponSystem.Collision;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action
{
    public interface IWeaponAction
    {
        /// <summary>
        /// 依存性注入をするためのメソッドです
        /// 独自にWeaponクラスを作成するときはAwake(), Start(), OnEnable()でコールしてください
        /// </summary>
        void Injection(Transform parent, Animator animator, IMagazine magazine);
    }
}