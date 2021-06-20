using Cinemachine;
using UnityEngine;
using WeaponSystem.Camera;
using WeaponSystem.Runtime;

namespace WeaponSystem.Cinemachine
{
    public class CinemachineReferenceCamera : MonoBehaviour, IReferenceCamera
    {
        private CinemachineBrain _brain;
        private CinemachineVirtualCamera _camera;

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
            var icmCam = _brain.ActiveVirtualCamera;
            _camera = icmCam.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
        }
    }
}