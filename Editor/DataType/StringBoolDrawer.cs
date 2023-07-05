using UnityEngine;
using UnityEditor;

namespace Meangpu.Datatype
{
    [CustomPropertyDrawer(typeof(StringWithBool))]
    public class StringBoolDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            SerializedProperty stringValue = property.FindPropertyRelative("stringValue");
            SerializedProperty boolValue = property.FindPropertyRelative("boolValue");

            const int column = 20;
            const int boolTickSpaceRation = 1;
            const int widthRatio = column - boolTickSpaceRation;

            float widthSize = position.width / column;

            const float offsetSize = 5;

            Rect pos1 = new(position.x, position.y, (widthSize * widthRatio) - offsetSize, position.height);
            Rect pos2 = new(position.x + (widthSize * widthRatio), position.y, widthSize - offsetSize, position.height);

            EditorGUI.PropertyField(pos1, stringValue, GUIContent.none);
            EditorGUI.PropertyField(pos2, boolValue, GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
}