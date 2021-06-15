using System;
using UnityEngine;
using WeaponSystem.Camera;
using WeaponSystem.Movement;
using WeaponSystem.Runtime;
using WeaponSystem.Scripts;

namespace WeaponSystem.Weapon.Muzzle
{
    [Serializable, AddTypeMenu("DefusingMuzzle")]
    public class DefusingMuzzle : IMuzzle
    {
        [SerializeField] private Transform reference;
        [SerializeField] private AccuracySetting[] settings = {new AccuracySetting()};

        public Vector3 Position => reference.position;
        public Vector3 Direction => reference.forward;
        public Quaternion Rotation => reference.rotation;

        public void Defuse()
        {
            var camera = Locator<IReferenceCamera>.Instance.Current.Center;
            var context = Locator<IPlayerContext>.Instance.Current;
            reference.rotation = Quaternion.LookRotation(camera.forward * 1000f);

            if (settings.Length <= 0) return;

            if (context?.IsAiming ?? false) return;

            foreach (var setting in settings)
            {
                if (setting.state == context.State)
                {
                    reference.rotation =
                        Quaternion.LookRotation(camera.rotation * setting.Defuse + camera.forward * setting.Distance);
                    return;
                }
            }
        }
    }
}