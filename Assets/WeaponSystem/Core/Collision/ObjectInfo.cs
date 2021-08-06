using UnityEngine;
using WeaponSystem.Core.Collision.ObjectMaterial;

namespace WeaponSystem.Core.Collision
{
    public readonly struct ObjectInfo
    {
        public readonly IObjectMaterial material;
        public readonly Transform transform;
        public readonly Vector3 normal;

        public ObjectInfo(IObjectMaterial material, Transform transform, Vector3 normal)
        {
            this.material = material;
            this.transform = transform;
            this.normal = normal;
        }
    }
}