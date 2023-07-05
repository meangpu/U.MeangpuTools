using UnityEngine;
using UnityEditor;

namespace Meangpu.Datatype
{
    [CustomPropertyDrawer(typeof(StringWithBoolTextArea))]
    public class StringBoolDrawerTextArea : PropertyDrawer
    {
        float textHeight;
        GUIStyle TextAreaStyle = new(EditorStyles.textArea);

        SerializedProperty stringValueProperty;
        SerializedProperty boolValue;

        GUIContent textAreaContent;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            stringValueProperty = property.FindPropertyRelative("stringValue");
            boolValue = property.FindPropertyRelative("boolValue");

            const int column = 20;
            const int boolTickSpaceRation = 1;
            const int widthRatio = column - boolTickSpaceRation;

            float widthSize = position.width / column;

            const float offsetSize = 5;

            Rect pos1 = new(position.x, position.y, (widthSize * widthRatio) - offsetSize, position.height);
            Rect pos2 = new(position.x + (widthSize * widthRatio), position.y, widthSize - offsetSize, position.height);

            stringValueProperty.stringValue = EditorGUI.TextArea(pos1, stringValueProperty.stringValue, TextAreaStyle);
            EditorGUI.PropertyField(pos2, boolValue, GUIContent.none);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            stringValueProperty = property.FindPropertyRelative("stringValue");
            textAreaContent = new(stringValueProperty.stringValue);
            textHeight = TextAreaStyle.CalcHeight(textAreaContent, EditorGUIUtility.currentViewWidth);
            return textHeight;
        }
    }
}
