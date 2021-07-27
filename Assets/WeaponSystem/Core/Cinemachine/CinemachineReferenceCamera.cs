using Cinemachine;
using UnityEngine;
using WeaponSystem.Core.Camera;
using WeaponSystem.Core.Runtime;

namespace WeaponSystem.Core.Cinemachine
{
    public class CinemachineReferenceCamera : ReferenceCameraBase
    {
        [SerializeField] private int updateVCamFrameRate = 50;

        private CinemachineBrain _brain;
        private CinemachineVirtualCamera _camera;

        private int _frameCount;

        public override float FovMultiple
        {
            get => _camera.m_Lens.FieldOfView / FieldOfView;
            set => _camera.m_Lens.FieldOfView = FieldOfView * Mathf.Abs(value);
        }

        public override UnityEngine.Camera Camera => _brain.OutputCamera;
        public override Transform Center => transform;

        private void Awake() => _brain = GetComponent<CinemachineBrain>();

        private void OnEnable() => Locator<ReferenceCameraBase>.Instance.Bind(this);

        private void OnDisable() => Locator<ReferenceCameraBase>.Instance.Unbind(this);
        
        private void Update()
        {
            _frameCount++;
            if (_frameCount % updateVCamFrameRate != 0) return;
            var icmCam = _brain.ActiveVirtualCamera;
            _camera = icmCam.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
        }
    }
}