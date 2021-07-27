using System;
using UnityEngine;
using WeaponSystem.Core.Collision;

namespace WeaponSystem.Core.Weapon.Bullet
{
    [Serializable, AddTypeMenu("Blank")]
    public class BlankBullet : IBullet
    {
        public void Shot(Vector3 position, Vector3 direction, IObjectPermission permission, IObjectGroup objectGroup)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.DrawLine(position, direction * 1000f, Color.red);
#endif
        }
    }
}