using UnityEditor;
using UnityEngine;
using WeaponSystem.Weapon.Recoil;

namespace WeaponSystem.Editor
{
    [CustomEditor(typeof(RecoilPatternData))]
    public class RecoilPatternDataInspector : UnityEditor.Editor
    {
        private float _scale = 1f;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var pattern = (RecoilPatternData) target;

            EditorGUILayout.LabelField("Recoil Pattern Graph");

            var rect = GUILayoutUtility.GetRect(100f, 200f);

            Vector3 current = Vector3.zero;
            rect.DrawVerticalLine();
            foreach (Vector2 dot in pattern.pattern)
            {
                current += new Vector3(dot.x, -dot.y) * (pattern.Height / 10f * _scale);
                rect.DrawWireDot(current + new Vector3(0f, rect.height / 2f));
            }

            EditorGUILayout.LabelField("Scale: ");
            _scale = EditorGUILayout.Slider(_scale, 0f, 1f);
        }
    }
}