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
        static bool _hierarchyHasFocus;
        static EditorWindow _hierarchyEditorWIndow;

        static HierarchyIconDisplay()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
            EditorApplication.update += OnEditorUpdate;
        }

        private static void OnEditorUpdate()
        {
            if (_hierarchyEditorWIndow == null) _hierarchyEditorWIndow = EditorWindow.GetWindow(Type.GetType("UnityEditor.SceneHierarchyWindow,UnityEditor"));
            _hierarchyHasFocus = EditorWindow.focusedWindow != null && EditorWindow.focusedWindow == _hierarchyEditorWIndow;
        }

        private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            if (EditorUtility.InstanceIDToObject(instanceID) is not GameObject obj) return;

            Component[] components = obj.GetComponents<Component>();
            if (components == null || components.Length == 0) return;

            Component component = components.Length > 1 ? components[1] : components[0];  // first custom component
            Type type = component.GetType();

            GUIContent content = EditorGUIUtility.ObjectContent(null, type);
            content.text = null;
            content.tooltip = type.Name;
            if (content.image == null) return;

            bool isSelected = Selection.instanceIDs.Contains(instanceID);
            bool isHovering = selectionRect.Contains(Event.current.mousePosition);

            Color color = UnityEditorBackgroundColor.Get(isSelected, isHovering, _hierarchyHasFocus);
            Rect backgroundRect = selectionRect;
            backgroundRect.width = 18.5f;
            EditorGUI.DrawRect(backgroundRect, color);

            EditorGUI.LabelField(selectionRect, content);
        }
    }
}