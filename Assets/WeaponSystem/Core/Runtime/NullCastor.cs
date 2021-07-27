namespace WeaponSystem.Core.Runtime
{
    public static class NullCastor
    {
        public static T NullCast<T>(this T obj) where T : UnityEngine.Object => obj != null ? obj : null;
    }
}