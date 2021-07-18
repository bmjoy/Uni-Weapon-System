namespace WeaponSystem.Input
{
    public interface IMovementInput
    {
        float Vertical { get; }
        float Horizontal { get; }
        bool IsSprint { get; }
        bool IsJump { get; }
        bool IsCrouch { get; }
    }
}