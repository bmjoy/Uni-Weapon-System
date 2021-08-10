using UnityEngine;
using WeaponSystem.Core.Movement;

namespace WeaponSystem.Core.Weapon.Muzzle
{
    public interface IMuzzle
    {
        public Vector3 Position { get; }
        public Vector3 Direction { get; }

        public Quaternion Rotation { get; }
        public void Defuse(IPlayerState state, bool isAim);
    }
}