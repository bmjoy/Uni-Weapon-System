using UnityEngine;
using System;

namespace WeaponSystem.Weapon.Recoil
{
    [Serializable, AddTypeMenu("None")]
    public class NoneRecoil : IRecoil
    {
        // ReSharper disable Unity.PerformanceAnalysis
        void IRecoil.Reset()
        {
#if DEBUG && UNITY_EDITOR
            Debug.Log("Lap!");
#endif
        }

        // ReSharper disable Unity.PerformanceAnalysis
        void IRecoil.Generate()
        {
#if DEBUG && UNITY_EDITOR
            Debug.Log("Generate!");
#endif
        }

        public void Easing() { }
    }
}