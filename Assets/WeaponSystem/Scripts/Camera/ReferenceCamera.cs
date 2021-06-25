using UnityEngine;
using WeaponSystem.Runtime;
using static UnityEngine.Mathf;

namespace WeaponSystem.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class ReferenceCamera : MonoBehaviour, IReferenceCamera
    {
        private void Awake() => Camera = GetComponent<UnityEngine.Camera>();

        private void OnEnable() => Locator<IReferenceCamera>.Instance.Bind(this);

        private void OnDisable() => Locator<IReferenceCamera>.Instance.Unbind(this);

        public float FieldOfView
        {
            get => Camera.fieldOfView;
            set => Camera.fieldOfView = Abs(value);
        }

        public UnityEngine.Camera Camera { get; private set; }

        public Transform Center => Camera.transform;
    }
}