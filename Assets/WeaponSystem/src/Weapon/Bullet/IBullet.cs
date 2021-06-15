using UnityEngine;

namespace WeaponSystem.Weapon.Bullet
{
    public interface IBullet
    {
        void Shot(Vector3 position, Vector3 direction);
    }
}