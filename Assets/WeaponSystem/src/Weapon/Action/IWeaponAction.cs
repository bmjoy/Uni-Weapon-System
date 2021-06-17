using UnityEngine;
using WeaponSystem.Movement;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action
{
    public interface IWeaponAction
    {
        /// <summary>
        /// 依存性注入をするための関数です
        /// 独自にWeaponクラスを作成するときはAwake(), OnEnable()でコールしてください
        /// </summary>
        /// <param name="parent">親オブジェクトの参照</param>
        /// <param name="animator">銃のモデルのAnimator</param>
        /// <param name="magazine">銃のマガジン。必要ない場合はNoneMagazineを渡す</param>
        /// <param name="context">Playerの状態を表すクラス</param>
        void Injection(Transform parent, Animator animator, IMagazine magazine);
    }
}