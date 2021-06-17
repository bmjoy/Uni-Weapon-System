using System;
using UnityEngine;

namespace WeaponSystem.Collision
{
    [Serializable]
    public class DamageCollision
    {
        public uint PlayerId => playerId;
        public uint TeamId => teamId;

        [SerializeField] private uint playerId;
        [SerializeField] private uint teamId;
    }
}