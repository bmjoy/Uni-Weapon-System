using UnityEditor;
using UnityEngine;
using WeaponSystem.Weapon.ShotgunPattern;

namespace WeaponSystem.Editor
{
    [CustomEditor(typeof(ShotgunDefusePatternData))]
    public class ShotgunDefusePatternDataInspector : UnityEditor.Editor
    {
        private float _scale = 10f;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var pattern = (ShotgunDefusePatternData) target;
            EditorGUILayout.LabelField("Shotgun Defuse Graph");
            
            var rect = GUILayoutUtility.GetRect(100f, 100f);
            rect.DrawHorizontalLine();
            rect.DrawVerticalLine();

            foreach (var offset in pattern)
            {
                rect.DrawWireDot(new Vector3(offset.x, -offset.y) * (_scale * 100f));
            }
            
            EditorGUILayout.LabelField("Distance: ");
            _scale = EditorGUILayout.Slider(_scale, 0f, 100f);
        }
    }
}