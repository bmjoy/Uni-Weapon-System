using System;
using UnityEngine;

namespace WeaponSystem.Weapon.Bullet
{
    [Serializable, AddTypeMenu("Blank")]
    public class BlankBullet : IBullet
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public void Shot(Vector3 position, Vector3 direction)
        {
#if DEBUG
            Debug.Log($"Shot! Pos: {position.ToString()}, Dir: {direction.ToString()}");
#endif
        }
        
    }
}