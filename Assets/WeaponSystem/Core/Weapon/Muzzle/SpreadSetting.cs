using UnityEngine;
using WeaponSystem.Core.Movement;

namespace WeaponSystem.Core.Weapon.Muzzle
{
    [CreateAssetMenu]
    public class SpreadSetting : ScriptableObject
    {
        [SerializeField] private Spread[] spreads;

        public Spread this[PlayerMovementState state]
        {
            get
            {
                foreach (var spread in spreads)
                {
                    if (spread.State == state) return spread;
                }

                return Spread.Default;
            }
        }
    }
}