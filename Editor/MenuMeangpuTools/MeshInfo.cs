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

        [MenuItem("MeangpuTools/EditorUtil/Mesh Info")]
        static void Init()
        {
            MeshInfo window = (MeshInfo)GetWindow(typeof(MeshInfo));
            window.titleContent.text = "Mesh Info";
        }

        void OnSelectionChange() => Repaint();

        void CountAllChildPoly(GameObject g)
        {
            g.TryGetComponent(out MeshFilter MeshFilter);
            g.TryGetComponent(out SkinnedMeshRenderer skinMesh);

            if (MeshFilter != null && MeshFilter.sharedMesh != null)
            {
                vertexCount += MeshFilter.sharedMesh.vertexCount;
                triangleCount += MeshFilter.sharedMesh.triangles.Length / 3;
                subMeshCount += MeshFilter.sharedMesh.subMeshCount;
            }

            if (skinMesh != null && skinMesh.sharedMesh != null)
            {
                vertexCount += skinMesh.sharedMesh.vertexCount;
                triangleCount += skinMesh.sharedMesh.triangles.Length / 3;
                subMeshCount += skinMesh.sharedMesh.subMeshCount;
            }

            foreach (Transform child in g.transform)
            {
                CountAllChildPoly(child.gameObject);
            }
        }

        void OnGUI()
        {
            GUIStyle boldStyle = new()
            {
                fontSize = 20,
                fontStyle = FontStyle.Bold,
            };
            boldStyle.normal.textColor = Color.yellow;

            vertexCount = 0;
            subMeshCount = 0;
            triangleCount = 0;

            if (!Selection.activeGameObject) return;
            GameObject g = Selection.activeGameObject;
            CountAllChildPoly(g);

            EditorGUILayout.LabelField(Selection.activeGameObject.name);
            EditorGUILayout.LabelField("Vertices: ", $"{vertexCount}");
            EditorGUILayout.LabelField("Triangles: ", $"{triangleCount}", boldStyle);
            EditorGUILayout.LabelField("SubMeshes: ", $"{subMeshCount}");
        }
    }
}