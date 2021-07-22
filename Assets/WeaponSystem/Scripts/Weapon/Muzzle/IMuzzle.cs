using UnityEngine;
using WeaponSystem.Scripts.Movement;

namespace WeaponSystem.Weapon.Muzzle
{
    public interface IMuzzle
    {
        public Vector3 Position { get; }
        public Vector3 Direction { get; }

        public Quaternion Rotation { get; }
        public void Defuse(IPlayerContext context);
    }
}