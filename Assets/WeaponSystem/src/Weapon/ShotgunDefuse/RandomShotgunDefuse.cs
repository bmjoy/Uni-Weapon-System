using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using static UnityEngine.Mathf;

namespace WeaponSystem.Weapon.ShotgunPattern
{
    [Serializable]
    public class RandomShotgunDefuse : IShotgunDefuse
    {
        [SerializeField] private int shotgunPellet = 3;
        [SerializeField] private AnimationCurve randomWeightCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        [SerializeField] private float defuseDistance = 10f;
        [SerializeField] private float defuseRadius = .5f;
        
        public IEnumerator<Vector3> GetEnumerator()
        {
            for (int i = 0; i < shotgunPellet; i++)
            {
                var rand = insideUnitCircle;
                rand.y = randomWeightCurve.Evaluate(Abs(rand.y));
                rand.x = randomWeightCurve.Evaluate(Abs(rand.x));
                yield return rand * (defuseRadius / defuseDistance);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}