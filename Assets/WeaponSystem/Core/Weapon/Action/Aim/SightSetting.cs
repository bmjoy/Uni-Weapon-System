using System;
using UnityEngine;

namespace WeaponSystem.Core.Weapon.Action.Aim
{
    [Serializable]
    public class SightSetting
    {
        public float fovMultiple = .9f;
        public Transform referencePoint;
    }
}