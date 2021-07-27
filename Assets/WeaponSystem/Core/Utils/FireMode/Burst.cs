using System;
using UnityEngine;

namespace WeaponSystem.Core.Utils.FireMode
{
    [Serializable, AddTypeMenu("Burst")]
    public class Burst : IFireMode
    {
        [SerializeField] private int maxCount = 1;
        private int _count;

        public bool Evaluate(bool isInput)
        {
            if (isInput == false)
            {
                _count = 0;
                return false;
            }

            if (_count < maxCount)
            {
                _count++;
                return true;
            }

            return false;
        }
    }
}