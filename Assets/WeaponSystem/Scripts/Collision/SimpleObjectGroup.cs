using System;
using UnityEngine;

namespace WeaponSystem.Collision
{
    public class SimpleObjectGroup : MonoBehaviour, IObjectGroup
    {
        [SerializeField] private int teamId;
        
        public Guid SelfId => Guid.NewGuid();

        public int GroupId => teamId;
    }
}