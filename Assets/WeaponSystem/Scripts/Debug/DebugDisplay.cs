using System;
using System.Collections.Generic;

namespace WeaponSystem.Scripts.Debug
{
    public static class DebugDisplay
    {
        public static bool IsLogging { get; set; } = true;
        public static int MaxLogMessage { get; set; } = 10;
        public static List<string> LogList => _logList;
        private static readonly List<string> _logList = new List<string>();

        public static void Log(string message)
        {
            if (IsLogging == false) return;
            if (MaxLogMessage < _logList.Count) _logList.RemoveAt(0);
            var now = DateTime.Now;
            _logList.Add($"[{now.ToLongTimeString()}] {message}");
        }

        public static void Clear() => _logList.Clear();
    }
}