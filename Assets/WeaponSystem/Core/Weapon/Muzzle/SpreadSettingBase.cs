using UnityEngine;
using WeaponSystem.Core.Movement;

namespace WeaponSystem.Core.Weapon.Muzzle
{
    public abstract class SpreadSettingBase : ScriptableObject
    {
        public abstract Spread this[PlayerMovementState state] { get; }
    }
}