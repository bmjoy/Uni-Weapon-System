using UnityEngine;
using WeaponSystem.Runtime;

namespace WeaponSystem.Camera
{
    public class ScopeCamera : MonoBehaviour, IScopeCamera
    {
        private UnityEngine.Camera _camera;

        public bool IsActive
        {
            get => _camera.enabled;
            set => _camera.enabled = value;
        }

        public float FieldOfView
        {
            get => _camera.fieldOfView;
            set => _camera.fieldOfView = value;
        }

        private void Awake() => _camera = GetComponent<UnityEngine.Camera>();
        private void OnEnable() => Locator<IScopeCamera>.Instance.Bind(this);
        private void OnDisable() => Locator<IScopeCamera>.Instance.Unbind(this);
    }
}