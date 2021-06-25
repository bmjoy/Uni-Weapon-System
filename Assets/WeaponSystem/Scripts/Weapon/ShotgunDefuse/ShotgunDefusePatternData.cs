using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem.Weapon.ShotgunPattern
{
    [CreateAssetMenu(fileName = "New ShotgunDefusePatternData", menuName = "GenericWeapon System/New ShotgunDefusePatternData", order = 0)]
    public class ShotgunDefusePatternData : ScriptableObject, IEnumerable<Vector3>
    {
        [SerializeField] private Vector2[] pattern;
        [SerializeField] private float defuseWidth;
        [SerializeField] private float defuseDistance;

        public IEnumerator<Vector3> GetPattern()
        {
            foreach (var offset in pattern)
            {
                yield return offset * (defuseWidth / defuseDistance);
            }
        }

        public IEnumerator<Vector3> GetEnumerator() => GetPattern();
        IEnumerator IEnumerable.GetEnumerator() => GetPattern();
    }
}