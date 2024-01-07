using UnityEngine;
using UnityEditor;

// [How could I count my poly / tri? - Questions & Answers - Unity Discussions](https://discussions.unity.com/t/how-could-i-count-my-poly-tri/164226)
namespace Meangpu
{
    public class MeshInfo : EditorWindow
    {
        private int vertexCount;
        private int subMeshCount;
        private int triangleCount;
        GUIStyle boldStyle;

        [MenuItem("MeangpuTools/Mesh Info")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            MeshInfo window = (MeshInfo)GetWindow(typeof(MeshInfo));
            window.titleContent.text = "Mesh Info";
        }
        void OnSelectionChange() => Repaint();
        void OnGUI()
        {
            if (Selection.activeGameObject && Selection.activeGameObject.GetComponent<MeshFilter>())
            {
                GUIStyle boldStyle = new()
                {
                    fontSize = 20,
                    fontStyle = FontStyle.Bold,
                };
                boldStyle.normal.textColor = Color.yellow;
                vertexCount = Selection.activeGameObject.GetComponent<MeshFilter>().sharedMesh.vertexCount;
                triangleCount = Selection.activeGameObject.GetComponent<MeshFilter>().sharedMesh.triangles.Length / 3;
                subMeshCount = Selection.activeGameObject.GetComponent<MeshFilter>().sharedMesh.subMeshCount;
                EditorGUILayout.LabelField(Selection.activeGameObject.name);
                EditorGUILayout.LabelField("Vertices: ", $"{vertexCount}");
                EditorGUILayout.LabelField("Triangles: ", $"{triangleCount}", boldStyle);
                EditorGUILayout.LabelField("SubMeshes: ", $"{subMeshCount}");
            }
        }
    }
}