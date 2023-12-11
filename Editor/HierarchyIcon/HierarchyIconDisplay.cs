using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

namespace Meangpu
{
    /// <summary>
    /// learn from this awesome guy [Next LEVEL Unity Hierarchy - YouTube](https://www.youtube.com/watch?v=EFh7tniBqkk)
    /// </summary>
    [InitializeOnLoad]
    public static class HierarchyIconDisplay
    {
        static HierarchyIconDisplay() => EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;

        private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            if (EditorUtility.InstanceIDToObject(instanceID) is not GameObject obj) return;

            // if (PrefabUtility.GetCorrespondingObjectFromOriginalSource(obj) != null) return; //! = toggle this for PREFAB = leave it as blue box if it is prefab

            Component[] components = obj.GetComponents<Component>();
            if (components == null || components.Length == 0) return;

            Component component = components.Length > 1 ? components[1] : components[0];  // first custom component
            if (component == null) return;

            Type type = component.GetType();

            if (type == typeof(CanvasRenderer) && components.Length > 2)
            {
                component = components[2];  // display tmp or image instead of canvas renderer
                type = component.GetType();
            }

            GUIContent content = EditorGUIUtility.ObjectContent(component, type);
            content.text = null;
            content.tooltip = type.Name;
            if (content.image == null) return;

            bool isSelected = Selection.instanceIDs.Contains(instanceID);
            bool isHovering = selectionRect.Contains(Event.current.mousePosition);

            Color color = UnityEditorBackgroundColor.Get(isSelected, isHovering);
            Rect backgroundRect = selectionRect;
            backgroundRect.width = 18.5f;
            EditorGUI.DrawRect(backgroundRect, color);

            EditorGUI.LabelField(selectionRect, content);
        }
    }
}