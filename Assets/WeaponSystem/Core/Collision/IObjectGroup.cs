using System;

namespace WeaponSystem.Core.Collision
{
    public interface IObjectGroup
    {
        Guid SelfId { get; }
        int GroupId { get; }
    }
}