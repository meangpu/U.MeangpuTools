using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Meangpu
{
    public class TriangleCountSorter : Editor
    {
        [MenuItem("MeangpuTools/PolyCount Sorter", priority = 9999)]
        public static void SortObjectsByTriangleCount()
        {
            MeshFilter[] _meshFilters = FindObjectsOfType<MeshFilter>();
            SkinnedMeshRenderer[] _skinMesh = FindObjectsOfType<SkinnedMeshRenderer>();


            var sortedObjects = _meshFilters
                .Where(meshFilter => meshFilter != null && meshFilter.sharedMesh != null)
                .OrderByDescending(meshFilter => meshFilter.sharedMesh.triangles.Length / 3)
                .Select(meshFilter => new { nowObject = meshFilter.gameObject, TriangleCount = meshFilter.sharedMesh.triangles.Length / 3 });

            var sortedSkinMesh = _skinMesh
                .Where(skinMeshFilter => skinMeshFilter != null && skinMeshFilter.sharedMesh != null)
                .OrderByDescending(skinMeshFilter => skinMeshFilter.sharedMesh.triangles.Length / 3)
                .Select(skinMeshFilter => new { nowObject = skinMeshFilter.gameObject, TriangleCount = skinMeshFilter.sharedMesh.triangles.Length / 3 })
                ;

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