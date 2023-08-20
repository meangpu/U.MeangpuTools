using UnityEngine;
using UnityEditor;

namespace Meangpu
{
    // learn from this awesome guy on internet [Read-only fields? - Unity Forum](https://forum.unity.com/threads/read-only-fields.68976/)

    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class PrivateReadOnly : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }

    public class ReadOnlyAttribute : PropertyAttribute { }
}
