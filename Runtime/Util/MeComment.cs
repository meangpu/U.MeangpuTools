using UnityEngine;

namespace Meangpu.Util
{
#if UNITY_EDITOR
    using UnityEditor;
    [CustomEditor(typeof(MeComment))]
    public class CommentsInspector : Editor
    {
        private MeComment Script { get { return target as MeComment; } }
        private GUIStyle style = new();

        // private static Color white = new(1f, 1f, 1f, 1f);
        private static Color blueColor = new(0.129f, 0.914f, 1f, 1f);

        public override void OnInspectorGUI()
        {
            if (serializedObject == null) return;

            style.wordWrap = true;
            style.normal.textColor = blueColor;

            serializedObject.Update();
            EditorGUILayout.Space();

            string text = EditorGUILayout.TextArea(Script.text, style);
            if (text != Script.text)
            {
                Undo.RecordObject(Script, "Edit Comments");
                Script.text = text;
            }

            EditorGUILayout.Space();
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif

    public class MeComment : MonoBehaviour
    {
        [Multiline] public string text;
    }
}