using System.Reflection;
using UnityEditor;
using UnityEngine;

// learn from part1: [Custom Attributes In Unity - YouTube](https://www.youtube.com/watch?v=M3tLr3EYIiE)
// learn from part2: [One Unity Editor To Rule Them All - YouTube](https://www.youtube.com/watch?v=DztxQiMr4EU)
// I stop it half ways cause it not working, so I comment it all out first
namespace Meangpu
{
    [CustomEditor(typeof(MonoBehaviour), editorForChildClasses: true)]
    public abstract class BaseEditor : Editor
    {
        static GUIStyle ms_titleStyle = null;
        ComponentAttribute m_componentAttribute;

        protected virtual void OnEnable()
        {
            m_componentAttribute ??= GetComponentAttribute(target);
        }

        public static ComponentAttribute GetComponentAttribute(Object target)
        {
            return target.GetType().GetCustomAttribute<ComponentAttribute>() ?? new ComponentAttribute(target.GetType().ToString());
        }

        public override void OnInspectorGUI()
        {
            HeaderGUI(m_componentAttribute);
            base.OnInspectorGUI();
        }

        public static void HeaderGUI(ComponentAttribute componentAttribute)
        {
            if (componentAttribute != null)
            {
                GUILayout.Space(10f);
                ms_titleStyle ??= new(GUI.skin.label)
                {
                    fontSize = 15,
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.MiddleCenter
                };
                GUIStyle titleStyle = new();

                // header section
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                GUILayout.Label(componentAttribute.Name, titleStyle);

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                // des section

                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                GUILayout.Box(componentAttribute.Description, GUILayout.Width(Screen.width * .8f));

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}