using UnityEngine;

namespace WeaponSystem.Core.Camera
{
    public abstract class ScopeCameraBase : MonoBehaviour
    {
        public abstract float FieldOfView { get; set; }
        public abstract bool IsActive { get; set; }
    }
}