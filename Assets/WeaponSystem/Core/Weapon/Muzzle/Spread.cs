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
        [SerializeField] private PlayerMovementState state = PlayerMovementState.Rest;
        [SerializeField, Range(0f, 100f)] private float moa = 1f;
        [SerializeField, Range(0f, 2f)] private float aimingSpreadMultiple;
        [SerializeField] private AnimationCurve verticalWeightCurve = Constant(0f, 1f, 1f);
        [SerializeField] private AnimationCurve horizontalWeightCurve = Constant(0f, 1f, 1f);

        public PlayerMovementState State => state;


        public Vector3 Defuse(bool isAim)
        {
            var defuse = new Vector3(Range(-1f, 1f), Range(-1f, 1f));
            defuse.y *= horizontalWeightCurve.Evaluate(Abs(defuse.y));
            defuse.x *= verticalWeightCurve.Evaluate(Abs(defuse.x));
            
            // 1moa = 100mで2.9cm以内
            defuse *= (0.29f / 100f) * moa;

            return defuse * (isAim ? aimingSpreadMultiple : 1f);
        }


        public float Distance => 100f;
    }
}