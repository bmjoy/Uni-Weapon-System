namespace WeaponSystem.Camera
{
    public interface ICameraRotate
    {
        float Vertical { get; set; }
        float VerticalOffset { get; set; }
        float Horizontal { get; set; }
        float HorizontalOffset { get; set; }
    }
}