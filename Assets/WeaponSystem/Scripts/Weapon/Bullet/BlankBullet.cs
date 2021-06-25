using System;
using UnityEngine;
using WeaponSystem.Collision;

namespace WeaponSystem.Weapon.Bullet
{
    [Serializable, AddTypeMenu("Blank")]
    public class BlankBullet : IBullet
    {
        public void Shot(Vector3 position, Vector3 direction, IObjectPermission permission, IObjectGroup objectGroup)
        {
#if UNITY_EDITOR
            Debug.DrawLine(position, direction * 1000f, Color.red);
#endif
        }
    }
}