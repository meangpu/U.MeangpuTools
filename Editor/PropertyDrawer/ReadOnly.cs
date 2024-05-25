using UnityEngine;
using UnityEditor;

namespace Meangpu
{
    // learn from this awesome guy on internet [Read-only fields? - Unity Forum](https://forum.unity.com/threads/read-only-fields.68976/)

    /// <summary>
    /// Allows you to add '[ReadOnly]' before a variable so that it is shown but not editable in the inspector.
    /// Small but useful script, to make your inspectors look pretty and useful :D
    /// </summary>
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
        }
    }
    // Learn More with this Tutorial: https://youtu.be/r3nwTGLHygI///
}
