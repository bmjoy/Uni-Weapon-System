using UnityEngine;
using WeaponSystem.Runtime;
using static UnityEngine.Mathf;

namespace WeaponSystem.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class ReferenceCamera : MonoBehaviour, IReferenceCamera
    {
        private UnityEngine.Camera _camera;
        private void Awake() => _camera = GetComponent<UnityEngine.Camera>();

        private void OnEnable() => Locator<IReferenceCamera>.Instance.Bind(this);

        private void OnDisable() => Locator<IReferenceCamera>.Instance.Unbind(this);

        public float FieldOfView
        {
            get => _camera.fieldOfView;
            set => _camera.fieldOfView = Abs(value);
        }

        public UnityEngine.Camera Camera => _camera;
        public Transform Center => _camera.transform;
    }
}