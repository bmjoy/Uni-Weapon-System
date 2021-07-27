namespace WeaponSystem.Core.Utils.Timer
{
    public class NoneWaitTimer : IRpmTimer
    {
        public bool IsValid => true;
        public void Update() { }

        public void Reset() { }

        public void Lap() { }
    }
}