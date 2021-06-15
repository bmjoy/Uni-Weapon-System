using UnityEngine;
using WeaponSystem.Input;
using WeaponSystem.Runtime;

namespace WeaponSystem.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class FPSCamera : MonoBehaviour,IPlayerCamera
    {
        [SerializeField] private RotateAxis vertical;
        [SerializeField] private RotateAxis horizontal;
        [SerializeField] private Transform playerBody;
        private UnityEngine.Camera _camera;

        private void Awake() => _camera = GetComponent<UnityEngine.Camera>();

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Locator<ICameraRotate>.Instance.Bind(this);
            Locator<IReferenceCamera>.Instance.Bind(this);
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Locator<ICameraRotate>.Instance.Unbind(this);
            Locator<IReferenceCamera>.Instance.Unbind(this);
        }

        private void Update()
        {
            vertical.Current += Locator<ICameraInput>.Instance.Current?.Vertical ?? 0f;
            horizontal.Current += Locator<ICameraInput>.Instance.Current?.Horizontal ?? 0f;
        }

        private void LateUpdate()
        {
            if (playerBody == false || playerBody == _camera.transform)
            {

                _camera.transform.rotation = horizontal[Vector3.up] * vertical[Vector3.left];
            }
            else
            {
                _camera.transform.localRotation = vertical[Vector3.left];
                playerBody.rotation = horizontal[Vector3.up];
            }
        }

        public float Vertical
        {
            get => vertical.Current;
            set => vertical.Current = value;
        }

        public float Horizontal
        {
            get => horizontal.Current;
            set => horizontal.Current = value;
        }

        public float FieldOfView
        {
            get => _camera.fieldOfView;
            set => _camera.fieldOfView = value;
        }


        public UnityEngine.Camera Camera { get; }
        public Transform Center => _camera.transform;
    }
}