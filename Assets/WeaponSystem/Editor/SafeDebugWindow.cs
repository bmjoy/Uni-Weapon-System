using UnityEditor;
using UnityEngine;
using WeaponSystem.Scripts.Debug;
using static UnityEngine.GUILayout;

namespace WeaponSystem.Editor
{
    public class SafeDebugWindow : EditorWindow
    {
        [MenuItem("WeaponSystem/SafeDebugWindow")]
        private static void ShowWindow()
        {
            var window = GetWindow<SafeDebugWindow>();
            window.titleContent = new GUIContent("SafeDebug");
            window.Show();
        }

        private void OnGUI()
        {
            SafeDebug.IsLog = Toggle(SafeDebug.IsLog, "Log");
            SafeDebug.IsError = Toggle(SafeDebug.IsError, "Error");
            SafeDebug.IsException = Toggle(SafeDebug.IsException, "Exception");
            SafeDebug.IsAssertion = Toggle(SafeDebug.IsAssertion, "Assertion");
        }
    }
}