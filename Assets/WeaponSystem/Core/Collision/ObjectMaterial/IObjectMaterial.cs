using UnityEngine;

namespace WeaponSystem.Core.Collision.ObjectMaterial
{
    public interface IObjectMaterial
    {
        public string GetMaterial(Vector3 position);
    }
}