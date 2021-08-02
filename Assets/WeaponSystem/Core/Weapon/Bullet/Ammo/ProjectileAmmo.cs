using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Core.Collision;
using WeaponSystem.Core.Collision.ObjectMaterial;

namespace WeaponSystem.Core.Weapon.Bullet.Ammo
{
    public abstract class ProjectileAmmo : MonoBehaviour
    {
        public abstract IObjectPermission ObjectPermission { get; set; }
        public abstract IObjectGroup ObjectGroup { get; set; }
        public abstract void AddForce(Vector3 force);

        public UnityEvent<ObjectInfo> onHit;

        protected abstract void OnHitObject(UnityEngine.Collision target);

        private void OnCollisionEnter(UnityEngine.Collision other)
        {
            ObjectInfo info = other.collider.TryGetComponent(out IObjectMaterial material)
                ? new ObjectInfo(material, other.transform, other.contacts[0].normal)
                : new ObjectInfo(null, other.transform, other.contacts[0].normal);

            onHit.Invoke(info);
            OnHitObject(other);
        }
    }
}