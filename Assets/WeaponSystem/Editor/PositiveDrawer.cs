using UnityEditor;
using UnityEngine;
using WeaponSystem.Attribute;
using static UnityEditor.EditorGUI;

namespace WeaponSystem.Editor
{
    [CustomPropertyDrawer(typeof(PositiveAttribute))]
    public class PositiveDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.floatValue = FloatField(position, Mathf.Clamp(property.floatValue, 0f, float.MaxValue));
        }
        
    }
}