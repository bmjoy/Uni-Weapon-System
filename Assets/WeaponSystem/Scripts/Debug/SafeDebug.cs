using System;

namespace WeaponSystem.Scripts.Debug
{
    public static class SafeDebug
    {
        public static bool IsLog { get; set; } = true;
        public static bool IsError { get; set; } = true;
        public static bool IsException { get; set; } = true;
        public static bool IsAssertion { get; set; } = true;


        public static void Log(this string msg)
        {
#if UNITY_EDITOR || DEBUG
            if (IsLog == false) return;
            UnityEngine.Debug.Log(msg);
#endif
        }

        public static void LogWarning(this string errMsg)
        {
#if UNITY_EDITOR || DEBUG
            if (IsLog == false) return;
            UnityEngine.Debug.LogWarning(errMsg);
#endif
        }

        public static void LogError(this string errMsg)
        {
#if UNITY_EDITOR || DEBUG
            if (IsLog == false) return;
            UnityEngine.Debug.LogError(errMsg);
#endif
        }

        public static void LogException(this Exception exception)
        {
#if UNITY_EDITOR || DEBUG
            if (IsLog == false) return;
            UnityEngine.Debug.LogException(exception);
#endif
        }

        public static void LogAssertion(this string assertMsg)
        {
#if UNITY_EDITOR || DEBUG
            if (IsLog == false) return;
            UnityEngine.Debug.LogAssertion(assertMsg);
#endif
        }
    }
}