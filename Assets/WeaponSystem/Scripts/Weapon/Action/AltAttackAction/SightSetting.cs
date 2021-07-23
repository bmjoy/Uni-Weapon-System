using System;
using UnityEngine;

namespace WeaponSystem.Weapon.Action.AltAttackAction
{
    [Serializable]
    public class SightSetting
    {
        public float fovMultiple = .9f;
        public Transform referencePoint;
    }
}