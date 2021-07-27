using UnityEngine;
using WeaponSystem.Core.Collision;

namespace WeaponSystem.Core.Weapon.Bullet
{
    public abstract class ProjectileAmmo : MonoBehaviour
    {
        public abstract IObjectPermission ObjectPermission { get; set; }
        public abstract IObjectGroup ObjectGroup { get; set; }
        public abstract void AddForce(Vector3 force);
    }
}