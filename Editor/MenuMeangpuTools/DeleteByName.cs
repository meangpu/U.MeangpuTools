using UnityEditor;
using UnityEngine;
using System.Linq;

namespace Meangpu
{
    public class DeleteByName : EditorWindow
    {
        public string targetObjectName;

        [MenuItem("MeangpuTools/EditorUtil/DeleteGameObjectByName")]
        public static void ShowWindow() => GetWindow<DeleteByName>("DeleteGameObjectByName");

        private void OnGUI()
        {
            targetObjectName = EditorGUILayout.TextField("TargetObjectName", targetObjectName);
            if (GUILayout.Button("DeleteAllObjectByName")) DeleteObjectByName();
        }

        void DeleteObjectByName()
        {
            var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == targetObjectName);
            foreach (var item in objects)
            {
                Debug.Log($"Do Delete{item.name}");
                DestroyImmediate(item);
            }
        }
    }
}