using System;

namespace WeaponSystem.Collision
{
    public interface IObjectGroup
    {
        Guid SelfId { get; }
        int GroupId { get; }
    }
}