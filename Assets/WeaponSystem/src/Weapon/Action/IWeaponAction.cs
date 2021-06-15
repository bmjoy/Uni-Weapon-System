using UnityEngine;
using WeaponSystem.Movement;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action
{
    public interface IWeaponAction
    {
        void Injection(Transform parent, Animator animator, IMagazine magazine, IPlayerContext context);
    }
}