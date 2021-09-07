using System;

namespace WeaponSystem.Core.Runtime.FireMode
{
    [Serializable, AddTypeMenu("FullAuto")]
    public class FullAuto : IFireMode
    {
        public bool Evaluate(bool input) => input;
    }
}