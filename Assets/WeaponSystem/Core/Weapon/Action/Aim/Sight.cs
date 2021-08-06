using UnityEngine;

namespace WeaponSystem.Core.Weapon.Action.Aim
{
    public class Sight : MonoBehaviour
    {
        [SerializeField] private float[] fovMultiple;
        [SerializeField] private int zoomMultipleIndex;
        [SerializeField] private Transform aimPoint;
        [SerializeField] private float duration = .1f;
        [SerializeField] private GameObject[] sightModels;

        public float Duration => duration;

        public Transform AimPoint => aimPoint;
        public float ZoomMultiples => fovMultiple[zoomMultipleIndex % fovMultiple.Length];

        public void ZoomChange() => zoomMultipleIndex = ++zoomMultipleIndex % fovMultiple.Length;

        private void OnEnable()
        {
            foreach (var model in sightModels) model.SetActive(true);
        }

        private void OnDisable()
        {
            foreach (var model in sightModels) model.SetActive(false);
        }
    }
}