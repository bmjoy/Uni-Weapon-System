using System;
using UnityEngine;
using WeaponSystem.Core.Movement;

namespace WeaponSystem.Core.Weapon.Muzzle
{
    /// <summary>
    /// 何もしないマズル
    /// VRとかの中にどうぞ
    /// </summary>
    [Serializable, AddTypeMenu("Identity")]
    public class IdentityMuzzle : IMuzzle
    {
        [SerializeField] private Transform reference;
        public Vector3 Position => reference.position;
        public Vector3 Direction => reference.forward;
        public Quaternion Rotation => reference.rotation;
        public void Defuse(IPlayerState state, bool isAim) { }
    }
}