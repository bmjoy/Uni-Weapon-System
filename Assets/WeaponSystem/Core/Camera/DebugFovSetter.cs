using UnityEngine;

namespace WeaponSystem.Core.Camera
{
    public class DebugFovSetter : MonoBehaviour
    {
        [SerializeField, Range(0f, 170f)] private float fov = 60f;

        private void Update() => ReferenceCameraBase.FieldOfView = fov;
    }
}