using UnityEngine;
using UnityEditor;
using System;

namespace Meangpu
{
    [InitializeOnLoad]
    public static class HierarchyIconDisplay
    {
        /// <summary>
        /// learn from this awesome guy [Next LEVEL Unity Hierarchy - YouTube](https://www.youtube.com/watch?v=EFh7tniBqkk) 
        /// </summary>

        static HierarchyIconDisplay() => EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;

        private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (obj == null) return;

            Component[] components = obj.GetComponents<Component>();
            if (components == null || components.Length == 0) return;

            Component component = components.Length > 1 ? components[1] : components[0];  // first custom component
            Type type = component.GetType();

            GUIContent content = EditorGUIUtility.ObjectContent(null, type);
            content.text = null;
            content.tooltip = type.Name;
            if (content.image == null) return;

            EditorGUI.LabelField(selectionRect, content);
        }
    }
}