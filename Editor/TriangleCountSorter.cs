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

            var sortedObjects = meshFilters
                .OrderByDescending(meshFilter => meshFilter.sharedMesh.triangles.Length / 3)
                .Select(meshFilter => new { ObjectName = meshFilter.gameObject.name, TriangleCount = meshFilter.sharedMesh.triangles.Length / 3 })
                .ToList();

            foreach (var obj in sortedObjects)
            {
                Debug.Log($"{obj.ObjectName}:{obj.TriangleCount}");
            }
        }
    }
}