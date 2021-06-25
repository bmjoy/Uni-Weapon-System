using System;
using UnityEngine;
using static UnityEngine.Mathf;
using static UnityEngine.Random;

namespace WeaponSystem.Weapon.Muzzle
{
    [Serializable]
    public class AccuracySetting
    {
        public PlayerMovementState state = PlayerMovementState.Rest;
        [SerializeField, Range(1f, 1000f)] private float accuracyDistance = 10f;
        [SerializeField, Range(.1f, 100f)] private float accuracyRadius = 1f;
        [SerializeField] private AnimationCurve verticalWeightCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        [SerializeField] private AnimationCurve horizontalWeightCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        public Vector3 Defuse
        {
            get
            {
                var defuse = new Vector3(Range(-1f, 1f), Range(-1f, 1f));
                defuse.y *= horizontalWeightCurve.Evaluate(Abs(defuse.y));
                defuse.x *= verticalWeightCurve.Evaluate(Abs(defuse.x));
                return defuse * accuracyRadius / accuracyDistance;
            }
        }

        public float Distance => accuracyDistance;
    }
}