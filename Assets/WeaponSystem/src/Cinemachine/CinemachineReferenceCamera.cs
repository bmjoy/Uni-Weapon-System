using Cinemachine;
using UnityEngine;
using WeaponSystem.Camera;
using WeaponSystem.Runtime;

namespace WeaponSystem.Cinemachine
{
    public class CinemachineReferenceCamera : MonoBehaviour, IReferenceCamera
    {
        [SerializeField] private int updateVCamFrameRate = 50;

        private CinemachineBrain _brain;
        private CinemachineVirtualCamera _camera;

        private int _frameCount;

        public float FieldOfView
        {
            get => _camera.m_Lens.FieldOfView;
            set => _camera.m_Lens.FieldOfView = Mathf.Abs(value);
        }

        public UnityEngine.Camera Camera => _brain.OutputCamera;
        public Transform Center => transform;

        private void Awake() => _brain = GetComponent<CinemachineBrain>();

        private void OnEnable() => Locator<IReferenceCamera>.Instance.Bind(this);

        private void OnDisable() => Locator<IReferenceCamera>.Instance.Unbind(this);


        private void Start()
        {
            var icmCam = _brain.ActiveVirtualCamera;
            var vCamObj = icmCam.VirtualCameraGameObject;
            _camera = vCamObj.GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            _frameCount++;
            if (_frameCount % updateVCamFrameRate != 0) return;
            var icmCam = _brain.ActiveVirtualCamera;
            _camera = icmCam.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
        }
    }
}