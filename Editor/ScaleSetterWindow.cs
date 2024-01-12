using UnityEditor;
using UnityEngine;

namespace Meangpu
{
    // learn make array from: [unity game engine - How to display & modify array in the Editor Window? - Stack Overflow](https://stackoverflow.com/questions/47753367/how-to-display-modify-array-in-the-editor-window)
    public class ScaleSetterWindow : EditorWindow
    {
        // create for vr-movie scale text, because it ui with bad parent object, so need some helper class to fix font size
        private Transform parentScaleRef;

        public GameObject[] childObjects = { };
        SerializedObject childSerializeObject;

        [MenuItem("MeangpuTools/EditorUtil/ScaleSetter")]
        public static void ShowWindow()
        {
            GetWindow<ScaleSetterWindow>("Scale Setter");
        }

        private void OnEnable()
        {
            ScriptableObject target = this;
            childSerializeObject = new SerializedObject(target);
        }

        private void OnGUI()
        {
            GUILayout.Label("Set Scale", EditorStyles.boldLabel);
            parentScaleRef = EditorGUILayout.ObjectField("Parent Scale Ref", parentScaleRef, typeof(Transform), true) as Transform;
            EditorGUILayout.Space();

            childSerializeObject.Update();
            SerializedProperty childProperty = childSerializeObject.FindProperty("childObjects");
            EditorGUILayout.PropertyField(childProperty, true); // True means show children
            childSerializeObject.ApplyModifiedProperties(); // Remember to apply modified properties

            if (GUILayout.Button("Set Scale"))
            {
                SetChildScale();
            }
        }

        private void SetChildScale()
        {
            if (parentScaleRef == null || childObjects == null)
            {
                Debug.LogError("Please set the parent scale reference and child objects.");
                return;
            }

            foreach (GameObject childObject in childObjects)
            {
                Transform oldChildParent = childObject.transform.parent;
                childObject.transform.SetParent(null, true);
                childObject.transform.localScale = parentScaleRef.localScale;
                childObject.transform.SetParent(oldChildParent, true);
            }
            Debug.Log("Child scales have been set to the parent scale.");
        }
    }
}