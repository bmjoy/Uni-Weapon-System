using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem.Weapon.ShotgunPattern
{
    [System.Serializable]
    public class PatternShotgunDefuse : IShotgunDefuse
    {
        [SerializeField] private ShotgunDefusePatternData patternData;
        public IEnumerator<Vector3> GetEnumerator() => patternData.GetPattern();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}