using UnityEngine;

namespace WeaponSystem.Weapon.Muzzle
{
    public class ScreenSpaceMuzzle : IMuzzle
    {
        public Vector3 Position { get; }
        public Vector3 Direction { get; }
        public Quaternion Rotation { get; }
        public void Defuse(){}
    }
}