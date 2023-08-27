using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

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

            int undoGroupIndex = Undo.GetCurrentGroup();
            Undo.IncrementCurrentGroup();

            GameObject gameObject = new(sourceComponent.GetType().Name);
            gameObject.transform.SetParent(sourceComponent.transform);
            gameObject.transform.localScale = Vector3.one;
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;

            Undo.RegisterCreatedObjectUndo(gameObject, "Created child object");

            if (!ComponentUtility.CopyComponent(sourceComponent) || !ComponentUtility.PasteComponentAsNew(gameObject))
            {
                Debug.LogError("Cannot extract component", sourceComponent.gameObject);
                Undo.CollapseUndoOperations(undoGroupIndex);
                Undo.PerformUndo();
                return;
            }

            Undo.DestroyObjectImmediate(sourceComponent);
            Undo.CollapseUndoOperations(undoGroupIndex);
        }
    }
}