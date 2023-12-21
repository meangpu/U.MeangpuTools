#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

namespace Meangpu.Util
{
    // [EXTRACTING Components In Unity - YouTube](https://www.youtube.com/watch?v=qDoevls1wmI)
    public static class ExtractComponentToChild
    {
        const string k_menuPath = "CONTEXT/Component/Extract";

        [MenuItem(k_menuPath, priority = 504)]
        public static void ExtractMenuOption(MenuCommand command)
        {
            Component sourceComponent = command.context as Component;
            ExtractComponent(sourceComponent);
        }

        public static void ExtractComponent(Component targetComponent, string namePrefix = "")
        {
            int undoGroupIndex = Undo.GetCurrentGroup();
            Undo.IncrementCurrentGroup();
            GameObject gameObject = CreateParentObj(targetComponent, namePrefix);
            Undo.RegisterCreatedObjectUndo(gameObject, "Created child object");
            ExtractOperation(targetComponent, undoGroupIndex, gameObject);
        }

        public static void ExtractComponent(List<Component> targetComponent, string namePrefix = "")
        {
            int undoGroupIndex = Undo.GetCurrentGroup();
            Undo.IncrementCurrentGroup();
            GameObject gameObject = CreateParentObj(targetComponent[0], namePrefix);
            Undo.RegisterCreatedObjectUndo(gameObject, "Created child object");
            foreach (Component component in targetComponent) ExtractOperation(component, undoGroupIndex, gameObject);
        }

        public static void ExtractComponent(Component[] targetComponent, string namePrefix = "")
        {
            int undoGroupIndex = Undo.GetCurrentGroup();
            Undo.IncrementCurrentGroup();
            GameObject gameObject = CreateParentObj(targetComponent[0], namePrefix);
            Undo.RegisterCreatedObjectUndo(gameObject, "Created child object");
            foreach (Component component in targetComponent) ExtractOperation(component, undoGroupIndex, gameObject);
        }

        private static GameObject CreateParentObj(Component targetComponent, string prefix)
        {
            GameObject gameObject = new($"{prefix}{targetComponent.GetType().Name}");
            gameObject.transform.SetParent(targetComponent.transform);
            gameObject.transform.localScale = Vector3.one;
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;
            return gameObject;
        }

        private static void ExtractOperation(Component targetComponent, int undoGroupIndex, GameObject gameObject)
        {
            if (!ComponentUtility.CopyComponent(targetComponent) || !ComponentUtility.PasteComponentAsNew(gameObject))
            {
                Debug.LogError("Cannot extract component", targetComponent.gameObject);
                Undo.CollapseUndoOperations(undoGroupIndex);
                Undo.PerformUndo();
                return;
            }
            Undo.DestroyObjectImmediate(targetComponent);
            Undo.CollapseUndoOperations(undoGroupIndex);
        }
    }
}
#endif