using System;
using UnityEngine;
using WeaponSystem.Core.Runtime;

namespace WeaponSystem.Core.Camera
{
    public class CameraFixedStar : MonoBehaviour, ICameraFixedStar
    {
        [Header("Rotate Settings")] [SerializeField]
        private AngleAxis verticalAxis;

        [SerializeField] private AngleAxis verticalOffsetAxis;
        [SerializeField] private AngleAxis horizontalAxis;
        [SerializeField] private AngleAxis horizontalOffsetAxis;
        [SerializeField] private Vector3 yaw = Vector3.up;

        public Quaternion FinalRotate { get; set; }

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
            var horizontal = horizontalAxis[yaw] * horizontalOffsetAxis[yaw];
            var vertical = verticalAxis[Pitch] * verticalOffsetAxis[Pitch];
            FinalRotate = horizontal * vertical;
        }

        private void LateUpdate()
        {
            _self.localRotation = FinalRotate;
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