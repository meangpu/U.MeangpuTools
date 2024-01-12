using UnityEngine;
using UnityEditor;

namespace Meangpu
{
    public static class MoveComponentTool
    {
        // learn from this [Move Component Context Tool For Unity - YouTube](https://www.youtube.com/watch?v=hOVqt_pvNCY)
        // use to make move component to top easier
        const string k_menuMoveToTop = "CONTEXT/Component/Move To Top";
        const string k_menuMoveToBottom = "CONTEXT/Component/Move To Bottom";

        [MenuItem(k_menuMoveToTop, validate = true)]
        public static bool MoveComponentToTopValidate(MenuCommand command)
        {
            Component[] components = ((Component)command.context).gameObject.GetComponents<Component>();
            for (int i = 0, length = components.Length; i < length; i++)
            {
                if (components[i] == ((Component)command.context) && i == 1)
                {
                    return false;
                }
            }
            return true;
        }

        [MenuItem(k_menuMoveToTop, priority = 501)]
        public static void MoveComponentToTop(MenuCommand command)
        {
            while (UnityEditorInternal.ComponentUtility.MoveComponentUp((Component)command.context)) ;
        }

        [MenuItem(k_menuMoveToBottom, validate = true)]
        public static bool MoveComponentToBottomValidate(MenuCommand command)
        {
            Component[] components = ((Component)command.context).gameObject.GetComponents<Component>();
            for (int i = 0, length = components.Length; i < length; i++)
            {
                if (components[i] == ((Component)command.context) && i == components.Length - 1)
                {
                    return false;
                }
            }
            return true;
        }

        [MenuItem(k_menuMoveToBottom, priority = 501)]
        public static void MoveComponentToBottom(MenuCommand command)
        {
            while (UnityEditorInternal.ComponentUtility.MoveComponentDown((Component)command.context)) ;
        }
    }
}