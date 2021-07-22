using System;
using UnityEngine;

namespace WeaponSystem.Weapon.Action.AltAttackAction
{
    [Serializable]
    public struct SightSetting
    {
        public float fovMultiple;
        public Transform referencePoint;
    }
}