using UnityEngine;

namespace WeaponSystem.Core.Collision.ObjectMaterial
{
    public class ObjectMaterial : MonoBehaviour, IObjectMaterial
    {
        private string _tag;
        public string GetMaterial(Vector3 position) => _tag ??= tag;
    }
}