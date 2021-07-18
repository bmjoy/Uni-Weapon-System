using UnityEngine;

namespace WeaponSystem.Weapon.Action.AltAttackAction
{
    public class Sight : MonoBehaviour
    {
        [SerializeField] private float zoomMultiple = .9f;
        [SerializeField] private Transform aimPoint;
        [SerializeField] private float duration = .1f;
        
        
        public float Duration => duration;

        public Transform AimPoint => aimPoint;
        public float ZoomMultiple => zoomMultiple;
    }
}