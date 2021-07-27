using UnityEngine;

namespace WeaponSystem.Core.Weapon.Action.Aim
{
    public abstract class SightBase : MonoBehaviour
    {
        public abstract Transform AimPoint { get; }
        public abstract float ZoomMultiples { get; }
    }
}