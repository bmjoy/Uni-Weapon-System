﻿using UnityEngine;
using WeaponSystem.Core.Runtime;

namespace WeaponSystem.Core.Camera
{
    public class ScopeCamera : ScopeCameraBase
    {
        [SerializeField] private UnityEngine.Camera overrideCamera;

        public override float FieldOfView
        {
            get => overrideCamera.fieldOfView;
            set => overrideCamera.fieldOfView = Mathf.Abs(value);
        }

        public override bool IsActive
        {
            get => overrideCamera.gameObject.activeSelf;
            set
            {
                var depth = Locator<ReferenceCameraBase>.Instance.Current?.Camera.depth + 1 ?? overrideCamera.depth;
                overrideCamera.depth = depth;
                overrideCamera.gameObject.SetActive(value);
            }
        }

        private void Awake() => overrideCamera = GetComponent<UnityEngine.Camera>();
    }
}