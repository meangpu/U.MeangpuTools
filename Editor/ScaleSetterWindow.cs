using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Meangpu
{
    // learn make array from: [unity game engine - How to display & modify array in the Editor Window? - Stack Overflow](https://stackoverflow.com/questions/47753367/how-to-display-modify-array-in-the-editor-window)
    public class ScaleSetterWindow : EditorWindow
    {
        // create for vr-movie scale text, because it ui with bad parent object, so need some helper class to fix font size
        private Transform ParentScaleRef;
        public GameObject[] ChildObjects = { };
        SerializedObject ChildSerializeObject;
        public Vector3 ScaleToSet;

        [MenuItem("MeangpuTools/EditorUtil/ScaleSetter")]
        public static void ShowWindow()
        {
            GetWindow<ScaleSetterWindow>("Scale Setter");
        }

        private void OnEnable()
        {
            ScriptableObject target = this;
            ChildSerializeObject = new SerializedObject(target);
        }

        private void OnGUI()
        {
            ParentScaleRef = EditorGUILayout.ObjectField("Parent Scale Ref", ParentScaleRef, typeof(Transform), true) as Transform;
            ScaleToSet = EditorGUILayout.Vector3Field("ScaleToSet", ScaleToSet);

            EditorGUILayout.Space();

            ChildSerializeObject.Update();
            SerializedProperty childProperty = ChildSerializeObject.FindProperty("ChildObjects");
            EditorGUILayout.PropertyField(childProperty, true); // True means show children
            ChildSerializeObject.ApplyModifiedProperties(); // Remember to apply modified properties

            if (GUILayout.Button("Set Scale To Parent ref")) SetChildScale();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("LoadParentToVector")) LoadParentScaleToVector3();
            if (GUILayout.Button("SetChildToVector")) SetChildToVector3();
            EditorGUILayout.EndHorizontal();
        }

        private void SetChildScale()
        {
            if (ParentScaleRef == null || ChildObjects == null)
            {
                MeLog.LogError("Please set the parent scale reference and child objects.");
                return;
            }

            foreach (GameObject childObject in ChildObjects)
            {
                Transform oldChildParent = childObject.transform.parent;
                Transform oldParentParent = ParentScaleRef.transform.parent;

                childObject.transform.SetParent(null, true);
                ParentScaleRef.transform.SetParent(null, true);

                childObject.transform.localScale = ParentScaleRef.localScale;

                childObject.transform.SetParent(oldChildParent, true);
                ParentScaleRef.transform.SetParent(oldParentParent, true);
            }
            MeLog.Log("Child scales have been set to the parent scale.");
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }

        void LoadParentScaleToVector3()
        {
            Transform oldParentParent = ParentScaleRef.transform.parent;

            ParentScaleRef.transform.SetParent(null, true);
            ScaleToSet = ParentScaleRef.localScale;

            ParentScaleRef.transform.SetParent(oldParentParent, true);
        }

        void SetChildToVector3()
        {
            if (ChildObjects == null)
            {
                MeLog.LogError("child error");
                return;
            }

            foreach (GameObject childObject in ChildObjects)
            {
                Transform oldChildParent = childObject.transform.parent;
                childObject.transform.SetParent(null, true);
                childObject.transform.localScale = ScaleToSet;
                childObject.transform.SetParent(oldChildParent, true);
            }
            MeLog.Log("Child scales have been set to the parent scale.");
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}