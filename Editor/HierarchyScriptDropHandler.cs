using UnityEngine;
using UnityEditor;
using System;

// from: [Bypass Unity Hierarchy Restrictions - YouTube](https://www.youtube.com/watch?v=FpOAcfULmTE)
[InitializeOnLoad]
public class HierarchyScriptDropHandler
{
    static HierarchyScriptDropHandler()
    {
        DragAndDrop.AddDropHandler(OnScriptHierarchyDrop);
    }

    private static DragAndDropVisualMode OnScriptHierarchyDrop(int dragInstanceId, string dropUponPath, bool perform)
    {
        MonoScript monoScript = GetScriptBeingDrag();
        if (monoScript != null)
        {
            if (perform)
            {
                GameObject gameObject = CreateAndRename(monoScript.name);
                Component component = gameObject.AddComponent(monoScript.GetClass());
            }
            return DragAndDropVisualMode.Copy;
        }
        return DragAndDropVisualMode.None;
    }

    static MonoScript GetScriptBeingDrag()
    {
        foreach (UnityEngine.Object dragObj in DragAndDrop.objectReferences)
        {
            if (dragObj is MonoScript monoScript)
            {
                Type scriptType = monoScript.GetClass();
                if (scriptType != null && scriptType.IsSubclassOf(typeof(MonoBehaviour))) return monoScript;
            }
        }
        return null;
    }

    public static GameObject CreateAndRename(string startingName)
    {
        GameObject gameObject = new(startingName);

        if (Selection.activeGameObject)
        {
            gameObject.transform.parent = Selection.activeGameObject.transform;
            gameObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        Selection.activeGameObject = gameObject;

        Undo.RegisterCreatedObjectUndo(gameObject, "Created Game Object");

        EditorApplication.delayCall += () =>
        {
            Type sceneHierarchyType = Type.GetType("UnityEditor.SceneHierarchyWindow, UnityEditor");
            EditorWindow.GetWindow(sceneHierarchyType).SendEvent(EditorGUIUtility.CommandEvent("Rename"));
        };

        return gameObject;
    }

}
