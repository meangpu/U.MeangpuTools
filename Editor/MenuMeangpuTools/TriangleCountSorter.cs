using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Meangpu
{
    public class TriangleCountSorter : Editor
    {
        [MenuItem("MeangpuTools/PolyCount Sorter")]
        public static void SortObjectsByTriangleCount()
        {
            MeshFilter[] meshFilters = FindObjectsOfType<MeshFilter>();
            SkinnedMeshRenderer[] skinMesh = FindObjectsOfType<SkinnedMeshRenderer>();

            var sortedObjects = meshFilters
                .OrderByDescending(meshFilter => meshFilter.sharedMesh.triangles.Length / 3)
                .Select(meshFilter => new { nowObject = meshFilter.gameObject, TriangleCount = meshFilter.sharedMesh.triangles.Length / 3 })
                .ToList();

            var sortedSkinMesh = skinMesh
                .OrderByDescending(meshFilter => meshFilter.sharedMesh.triangles.Length / 3)
                .Select(meshFilter => new { nowObject = meshFilter.gameObject, TriangleCount = meshFilter.sharedMesh.triangles.Length / 3 })
                .ToList();

            Debug.Log("======= MESH FILER =======");
            foreach (var obj in sortedObjects)
            {
                Debug.Log($"{obj.nowObject.name}:{obj.TriangleCount}", obj.nowObject);
            }
            Debug.Log("--------------------------");

            Debug.Log("======= SKIN Mesh =======");
            foreach (var obj in sortedSkinMesh)
            {
                Debug.Log($"{obj.nowObject.name}:{obj.TriangleCount}", obj.nowObject);
            }
            Debug.Log("--------------------------");
        }
    }
}