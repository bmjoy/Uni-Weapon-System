using UnityEngine;
using WeaponSystem.Core.Input;
using WeaponSystem.Core.Runtime;

namespace WeaponSystem.Core.Camera
{
    
    
    public class CameraFixedStar : MonoBehaviour, ICameraFixedStar
    {
        [SerializeField] private AngleAxis verticalAxis;
        [SerializeField] private AngleAxis verticalOffsetAxis;
        [SerializeField] private AngleAxis horizontalAxis;
        [SerializeField] private AngleAxis horizontalOffsetAxis;
        [SerializeField] private Vector3 yaw = Vector3.up;
        private Transform _self;

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Locator<ICameraFixedStar>.Instance.Bind(this);
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Locator<ICameraFixedStar>.Instance.Unbind(this);
        }

        private void Awake() => _self = transform;

        private void Update()
        {
            verticalAxis.Current += Locator<ICameraInput>.Instance.Current?.Vertical ?? 0f;
            horizontalAxis.Current += Locator<ICameraInput>.Instance.Current?.Horizontal ?? 0f;
        }

        private void LateUpdate()
        {
            var horizontal = horizontalAxis[yaw] * horizontalOffsetAxis[yaw];
            var vertical = verticalAxis[Pitch] * verticalOffsetAxis[Pitch];

            _self.rotation = horizontal * vertical;
        }

        public Vector3 Yaw
        {
            get => yaw;
            set => yaw = value.normalized;
        }

        public Vector3 Pitch => Vector3.ProjectOnPlane(Vector3.left, yaw);

        public float Vertical
        {
            get => verticalAxis.Current;
            set => verticalAxis.Current = value;
        }

        public float VerticalOffset
        {
            get => verticalOffsetAxis.Current;
            set => verticalOffsetAxis.Current = value;
        }

        public float Horizontal
        {
            get => horizontalAxis.Current;
            set => horizontalAxis.Current = value;
        }

        public float HorizontalOffset
        {
            get => horizontalOffsetAxis.Current;
            set => horizontalOffsetAxis.Current = value;
        }
    }
}