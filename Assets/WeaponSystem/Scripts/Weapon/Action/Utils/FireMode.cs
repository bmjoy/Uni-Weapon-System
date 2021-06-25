using System;
using UnityEngine;

namespace WeaponSystem.Weapon.Action
{
    public interface IFireMode
    {
        bool Evaluate(bool input);
    }

    [Serializable, AddTypeMenu("FullAuto")]
    public class FullAuto : IFireMode
    {
        public bool Evaluate(bool input) => input;
    }

    [Serializable, AddTypeMenu("SemiAuto")]
    public class SemiAuto : IFireMode
    {
        private bool _isKeyUp;
        
        public bool Evaluate(bool input)
        {
            if (input == false)
            {
                _isKeyUp = true;
                return false;
            }

            if (_isKeyUp == false)return false;

            _isKeyUp = false;
            
            return true;
        }
    }


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