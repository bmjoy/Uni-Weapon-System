using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem.Core.Weapon.ShotgunDefuse
{
    public abstract class ShotgunDefuseBase :ScriptableObject , IEnumerable<Vector3>
    {
        public abstract IEnumerator<Vector3> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}