﻿using UnityEngine;

namespace WeaponSystem.Core.Camera
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private Transform target;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _transform.parent = target;
        }

        private void LateUpdate()
        {
            _transform.localPosition = offset;
        }
    }
}