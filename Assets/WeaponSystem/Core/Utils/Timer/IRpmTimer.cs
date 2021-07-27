namespace WeaponSystem.Core.Utils.Timer
{
    public interface IRpmTimer
    {
        public bool IsValid { get; }
        public void Update();
        public void Reset();
        public void Lap();
    }
}