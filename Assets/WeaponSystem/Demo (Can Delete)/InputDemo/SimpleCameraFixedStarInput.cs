using UnityEngine;
using WeaponSystem.Core.Camera;

namespace WeaponSystem.InputDemo
{
    public class SimpleCameraFixedStarInput : MonoBehaviour
    {
        [SerializeField, Range(0f, 100f)] private float sensitivity = 10f;
        [SerializeField] private string verticalAxisName = "Mouse X";
        [SerializeField] private string horizontalAxisName = "Mouse Y";

        private ICameraFixedStar _fixedStar;

        private void Awake() => _fixedStar = GetComponent<ICameraFixedStar>();

        private void Update()
        {
            _fixedStar.Horizontal += UnityEngine.Input.GetAxisRaw(verticalAxisName) * Mathf.Deg2Rad * sensitivity;
            _fixedStar.Vertical += UnityEngine.Input.GetAxisRaw(horizontalAxisName) * Mathf.Deg2Rad * sensitivity;
        }
    }
}