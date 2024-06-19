using UnityEditor;
using UnityEngine;

namespace Meangpu
{
    // from: [Required For Unity Development - YouTube](https://www.youtube.com/watch?v=BN6NXMHJ8v0)
    [CustomPropertyDrawer(typeof(RequireAttribute))]
    public class RequireAttributeDrawer : PropertyDrawer
    {
        readonly Color _errorColor = new(1, .2f, .2f, .1f);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (IsFieldEmpty(property))
            {
                float height = EditorGUIUtility.singleLineHeight * 2f;
                height += base.GetPropertyHeight(property, label);
                return height;
            }
            else
            {
                return base.GetPropertyHeight(property, label);
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!IsFieldSupport(property))
            {
                Debug.LogError("Require attribute only support string and object reference");
                return;
            }

            if (IsFieldEmpty(property))
            {
                position.height = EditorGUIUtility.singleLineHeight * 2f;
                position.height += base.GetPropertyHeight(property, label);

                EditorGUI.HelpBox(position, "Require", MessageType.Error);
                EditorGUI.DrawRect(position, _errorColor);

                position.height = base.GetPropertyHeight(property, label);
                position.y += EditorGUIUtility.singleLineHeight * 2f;
            }
            EditorGUI.PropertyField(position, property, label);
        }

        bool IsFieldEmpty(SerializedProperty property)
        {
            if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null) return true;
            if (property.propertyType == SerializedPropertyType.String && string.IsNullOrEmpty(property.stringValue)) return true;

            return false;
        }

        bool IsFieldSupport(SerializedProperty property)
        {
            return property.propertyType == SerializedPropertyType.String || property.propertyType == SerializedPropertyType.ObjectReference;
        }
    }
}