using UnityEngine;
using UnityEditor;

namespace Meangpu
{
    // learn from [Quick Property Menus In Unity - YouTube](https://www.youtube.com/watch?v=hBrLsyLGaB4)
    // use by right click vector 3 and set all to zero or one
    public static class RightClickResetFieldValue
    {
        [InitializeOnLoadMethod] public static void Init() => EditorApplication.contextualPropertyMenu += OnPropertyContextMenu;

        private static void OnPropertyContextMenu(GenericMenu menu, SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Vector3:
                    menu.AddItem(new GUIContent("Zero"), false, () =>
                    {
                        property.vector3Value = Vector3.zero;
                        property.serializedObject.ApplyModifiedProperties();
                    });

                    menu.AddItem(new GUIContent("One"), false, () =>
                    {
                        property.vector3Value = Vector3.one;
                        property.serializedObject.ApplyModifiedProperties();
                    });
                    break;
                case SerializedPropertyType.Vector2:
                    menu.AddItem(new GUIContent("Zero"), false, () =>
                    {
                        property.vector2Value = Vector2.zero;
                        property.serializedObject.ApplyModifiedProperties();
                    });

                    menu.AddItem(new GUIContent("One"), false, () =>
                    {
                        property.vector3Value = Vector2.one;
                        property.serializedObject.ApplyModifiedProperties();
                    });
                    break;
                case SerializedPropertyType.Quaternion:
                    menu.AddItem(new GUIContent("RESET_ZERO"), false, () =>
                    {
                        property.quaternionValue = Quaternion.identity;
                        property.serializedObject.ApplyModifiedProperties();
                    });
                    break;
                default:
                    return;
            }
        }
    }
}