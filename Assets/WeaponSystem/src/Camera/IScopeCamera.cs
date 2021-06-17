namespace WeaponSystem.Camera
{
    public interface IScopeCamera
    {
        bool IsActive { get; set; }
        float FieldOfView { get; set; }
    }
}