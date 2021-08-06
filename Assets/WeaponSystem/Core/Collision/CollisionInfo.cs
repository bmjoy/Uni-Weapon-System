using System;
using UnityEngine;
using WeaponSystem.Core.Collision.ObjectMaterial;

namespace WeaponSystem.Core.Collision
{
    [Serializable]
    public struct CollisionInfo
    {
        public IObjectMaterial ObjectMaterial { get; }
        public Vector3 Normal { get; }
        public Transform Transform { get; }

        public CollisionInfo(IObjectMaterial objectMaterial, Vector3 normal, Transform transform)
        {
            this.ObjectMaterial = objectMaterial;
            this.Normal = normal;
            this.Transform = transform;
        }
    }
}