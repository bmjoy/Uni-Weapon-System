﻿using UnityEngine;

namespace WeaponSystem.Weapon.Action.AltAttackAction
{
    public class Sight : MonoBehaviour
    {
        [SerializeField] private SightSetting[] settings = new[] {new SightSetting()};
        [SerializeField] private int zoomMultipleIndex;
        [SerializeField] private Transform aimPoint;
        [SerializeField] private float duration = .1f;
        [SerializeField] private GameObject[] sightModels;


        public float Duration => duration;

        public Transform AimPoint => aimPoint;
        public float ZoomMultiples => settings[zoomMultipleIndex = zoomMultipleIndex % settings.Length].fovMultiple;

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