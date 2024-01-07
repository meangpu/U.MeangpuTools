using UnityEngine;
using UnityEditor;

namespace Meangpu
{
    // [View triangle count of a scene? - Questions & Answers - Unity Discussions](https://discussions.unity.com/t/view-triangle-count-of-a-scene/56893/3)
    public class CountPolygons : Editor
    {
        [MenuItem("GameObject/Count Polygons")]
        public static void CountPolygon()
        {
            GameObject go;

            Object prefabRoot = PrefabUtility
                .GetCorrespondingObjectFromSource(Selection.activeGameObject);

            if (prefabRoot != null)
                go = (GameObject)prefabRoot;
            else
                go = Selection.activeGameObject;

            MeshFilter[] meshFilters = go.GetComponentsInChildren<MeshFilter>();
            int polyCount = 0;
            foreach (MeshFilter meshFilter in meshFilters)
            {
                Mesh mesh = meshFilter.sharedMesh;
                polyCount += mesh.triangles.Length / 3;
            }

            if (meshFilters.Length > 0)
            {
                Debug.Log("Object " + go.name + " contains <b><color=yellow>" + meshFilters.Length
                                + " meshes </color></b> with total <b><color=red>"
                                + polyCount + " triangles</color></b>");
            }
            else
            {
                Debug.Log("<b><color=green>Object " + go.name + " does not contain any mesh - keep looking</color></b>");
            }
        }
    }
}