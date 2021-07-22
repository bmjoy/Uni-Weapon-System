using System.Text;
using UnityEngine;
using static UnityEngine.GUILayout;

namespace WeaponSystem.Scripts.Debug
{
    public class SceneLogger : MonoBehaviour
    {
        [SerializeField] private bool isLogging;

#if DEBUG || UNITY_EDITOR
        private void OnGUI()
        {
            isLogging = Toggle(isLogging, "IsLogging");
            DebugDisplay.IsLogging = isLogging;
            if (DebugDisplay.IsLogging == false) return;

            var sb = new StringBuilder();
            foreach (var log in DebugDisplay.LogList) sb.AppendLine(log);
            TextArea(sb.ToString(), Width(500));
        }
#endif
    }
}