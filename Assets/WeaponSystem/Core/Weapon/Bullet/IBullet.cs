using UnityEngine;
using WeaponSystem.Core.Collision;

namespace WeaponSystem.Core.Weapon.Bullet
{
    public interface IBullet
    {
        void Shot(Vector3 position, Vector3 direction, IObjectPermission permission, IObjectGroup objectGroup);
    }
}