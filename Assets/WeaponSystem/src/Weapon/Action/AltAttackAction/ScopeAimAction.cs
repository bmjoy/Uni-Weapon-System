using UnityEngine;
using WeaponSystem.Movement;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon.Action.AltAttackAction
{
    public class ScopeAimAction : IAltAttackAction
    {
        public void Injection(Transform parent, Animator animator, IMagazine magazine) { }

        public void Action(bool isAction, IPlayerContext context) { }
    }
}