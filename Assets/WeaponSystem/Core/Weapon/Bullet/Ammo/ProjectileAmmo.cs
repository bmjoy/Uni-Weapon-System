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

        public UnityEvent<ContactPoint[]> onHit;

        protected abstract void OnHitObject(UnityEngine.Collision target);


        private void OnCollisionEnter(UnityEngine.Collision target)
        {
            onHit.Invoke(target.contacts);
            OnHitObject(target);
        }
    }
}