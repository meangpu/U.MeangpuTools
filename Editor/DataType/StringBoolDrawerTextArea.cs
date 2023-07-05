using UnityEngine;
using UnityEditor;

namespace Meangpu.Datatype
{
    [CustomPropertyDrawer(typeof(StringWithBoolTextArea))]
    public class StringBoolDrawerTextArea : PropertyDrawer
    {
        float textHeight;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            GUIStyle TextAreaStyle = new(EditorStyles.textArea);

            SerializedProperty boolValue = property.FindPropertyRelative("boolValue");
            SerializedProperty stringValue = property.FindPropertyRelative("stringValue");

            const int column = 20;
            const int boolTickSpaceRation = 1;
            const int widthRatio = column - boolTickSpaceRation;

            float widthSize = position.width / column;

            const float offsetSize = 5;

            Rect pos1 = new(position.x, position.y, (widthSize * widthRatio) - offsetSize, position.height);
            Rect pos2 = new(position.x + (widthSize * widthRatio), position.y, widthSize - offsetSize, position.height);

            stringValue.stringValue = EditorGUI.TextArea(pos1, stringValue.stringValue, TextAreaStyle);
            EditorGUI.PropertyField(pos2, boolValue, GUIContent.none);

            GUIContent guiContent = new(stringValue.stringValue);
            textHeight = TextAreaStyle.CalcHeight(guiContent, EditorGUIUtility.currentViewWidth);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => textHeight;
    }
}
