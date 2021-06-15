using UnityEngine;
using WeaponSystem.Input;
using WeaponSystem.Runtime;


namespace WeaponSystem.Camera
{
    public enum ViewMode
    {
        FPS,
        TPS
    }

    [RequireComponent(typeof(UnityEngine.Camera))]
    public class SwitchingCamera : MonoBehaviour, IPlayerCamera
    {
        [SerializeField] private RotateAxis vertical = new RotateAxis(0f, -90f, 90f, true);
        [SerializeField] private RotateAxis horizontal;
        [SerializeField] private Transform playerBody;
        [SerializeField] private Transform cameraCenter;
        [SerializeField] private Vector2 offset = new Vector2(-1, 0f);
        [SerializeField, Range(0f, -100f)] private float maxDistance = -1f;
        [SerializeField] private ViewMode viewMode;
        [SerializeField] private float dumpTime = .1f;
        [SerializeField] private LayerMask clearShotMask;
        private UnityEngine.Camera _camera;
        private Transform _camTransform;

        private void Awake()
        {
            _camera = GetComponent<UnityEngine.Camera>();
            _camTransform = _camera.transform;

            if (cameraCenter == null)
            {
                var center = new GameObject("RotateCenter").transform;
                center.parent = playerBody == null ? center.transform : playerBody;
                cameraCenter = center;
                cameraCenter.localPosition = Vector3.zero;
            }

            _camTransform.parent = cameraCenter;
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Locator<IPlayerCamera>.Instance.Bind(this);
            Locator<ICameraRotate>.Instance.Bind(this);
            Locator<IReferenceCamera>.Instance.Bind(this);
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Locator<IPlayerCamera>.Instance.Unbind(this);
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
            if (cameraCenter == playerBody || playerBody == false)
            {
                cameraCenter.localRotation = horizontal[Vector3.up] * vertical[Vector3.left];
            }
            else
            {
                cameraCenter.localRotation = vertical[Vector3.left];
                playerBody.rotation = horizontal[Vector3.up];
            }

            switch (viewMode)
            {
                case ViewMode.FPS:
                    OnFpsView();
                    return;
                case ViewMode.TPS:
                    OnTpsView();
                    return;
            }
        }

        private void OnFpsView()
        {
            if (_camTransform.localPosition.sqrMagnitude < Mathf.Epsilon) return;

            _camTransform.localPosition =
                Vector3.Lerp(_camTransform.localPosition, Vector3.zero, Time.deltaTime / dumpTime);
        }

        private void OnTpsView()
        {
            Ray ray = new Ray(cameraCenter.position, _camTransform.position - cameraCenter.position);

            Debug.DrawRay(ray.origin, ray.direction * Mathf.Abs(maxDistance));

            if (Physics.SphereCast(ray, .5f, out RaycastHit hit, maxDistance, clearShotMask))
            {
                var distance = -Mathf.Abs(Vector3.Distance(cameraCenter.position, hit.point));
                _camTransform.localPosition = new Vector3(offset.x, offset.y, distance);
            }
            else
            {
                var time = Time.deltaTime / dumpTime;
                var to = new Vector3(offset.x, offset.y, maxDistance);
                _camTransform.localPosition = Vector3.Lerp(_camTransform.localPosition, to, time);
            }
        }

        public ViewMode ViewMode
        {
            get => viewMode;
            set => viewMode = value;
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
            set => _camera.fieldOfView = Mathf.Clamp(value, 0f, 120f);
        }

        public UnityEngine.Camera Camera => _camera;
        public Transform Center => _camera.transform;
    }
}