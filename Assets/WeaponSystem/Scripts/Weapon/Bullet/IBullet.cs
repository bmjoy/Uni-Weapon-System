using UnityEngine;
using WeaponSystem.Collision;

namespace WeaponSystem.Weapon.Bullet
{
    public interface IBullet
    {
        void Shot(Vector3 position, Vector3 direction, IObjectPermission permission, IObjectGroup objectGroup);
    }
}