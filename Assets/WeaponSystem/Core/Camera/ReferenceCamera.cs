using UnityEngine;
using WeaponSystem.Core.Runtime;
using static UnityEngine.Mathf;

namespace WeaponSystem.Core.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class ReferenceCamera : ReferenceCameraBase
    {
        private UnityEngine.Camera _camera;
        private void Awake() => _camera = GetComponent<UnityEngine.Camera>();

        private void OnEnable() => Locator<ReferenceCameraBase>.Instance.Bind(this);

        private void OnDisable() => Locator<ReferenceCameraBase>.Instance.Unbind(this);

        public override float FovMultiple
        {
            get => _fovMultiple;
            set => _fovMultiple = Abs(value);
        }

        private float _fovMultiple;

        private void LateUpdate() => _camera.fieldOfView = FovMultiple * FieldOfView;

        public override UnityEngine.Camera Camera => _camera;

        public override Transform Center => Camera.transform;
    }
}