using UnityEngine;
using UnityEditor;

namespace Meangpu.Util
{
    // [Try SEEING DOUBLE In this Unity Tutorial - YouTube](https://www.youtube.com/watch?v=Jfv7K54ZNko)
    public static class DuplicateComponentTool
    {
        const string k_menuPath = "CONTEXT/Component/Duplicate";
        [MenuItem(k_menuPath, priority = 503)]
        public static void ComponentDuplicate(MenuCommand command)
        {
            Component sourceComponent = command.context as Component;
            Component newComponent = Undo.AddComponent(sourceComponent.gameObject, sourceComponent.GetType());

            SerializedObject source = new(sourceComponent);
            SerializedObject target = new(newComponent);
            SerializedProperty iterator = source.GetIterator();
            while (iterator.NextVisible(true))
            {
                target.CopyFromSerializedProperty(iterator);
            }
            target.ApplyModifiedProperties();
        }

        [MenuItem(k_menuPath, validate = true)]
        public static bool DuplicateOptionValidate(MenuCommand command) => DoesComponentAllowMultiples(command.context as Component);

        static bool DoesComponentAllowMultiples(Component component)
        {
            System.Type componentType = component.GetType();
            DisallowMultipleComponent[] attributes = (DisallowMultipleComponent[])componentType.GetCustomAttributes(typeof(DisallowMultipleComponent), true);
            return attributes.Length == 0;
        }
    }
}