using UnityEngine;

namespace WeaponSystem.Camera
{
    public interface IReferenceCamera
    {
        float FieldOfView { get; set; }
        UnityEngine.Camera Camera { get; }

        Transform Center { get; }
    }
}