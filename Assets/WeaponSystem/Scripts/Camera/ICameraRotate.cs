namespace WeaponSystem.Camera
{
    public interface ICameraFixedStar
    {
        float Vertical { get; set; }
        float VerticalOffset { get; set; }
        float Horizontal { get; set; }
        float HorizontalOffset { get; set; }
    }
}