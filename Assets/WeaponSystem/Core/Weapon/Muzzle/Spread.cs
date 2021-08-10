using System;
using UnityEngine;
using WeaponSystem.Core.Movement;
using static UnityEngine.Mathf;
using static UnityEngine.Random;
using static UnityEngine.AnimationCurve;

namespace WeaponSystem.Core.Weapon.Muzzle
{
    [Serializable]
    public class Spread
    {
        public static readonly Spread Default = new Spread();
        [SerializeField] private PlayerMovementAction action = PlayerMovementAction.Rest;
        [SerializeField, Range(1f, 1000f)] private float distance = 10f;
        [SerializeField, Range(.1f, 100f)] private float radius = 1f;
        [SerializeField, Range(0f, 2f)] private float aimingSpreadMultiple;
        [SerializeField] private AnimationCurve verticalWeightCurve = Constant(0f, 1f, 1f);
        [SerializeField] private AnimationCurve horizontalWeightCurve = Constant(0f, 1f, 1f);

        public PlayerMovementAction Action => action;

        public Vector3 Defuse(bool isAim)
        {
            var defuse = new Vector3(Range(-1f, 1f), Range(-1f, 1f));
            defuse.y *= horizontalWeightCurve.Evaluate(Abs(defuse.y));
            defuse.x *= verticalWeightCurve.Evaluate(Abs(defuse.x));
            defuse *= radius / distance;
            return defuse * (isAim ? aimingSpreadMultiple : 1f);
        }

        public float Distance => distance;
    }
}