using UnityEngine;
using EasyButtons;

#if UNITY_EDITOR
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class MeshCombiner : MonoBehaviour
{

    [Button]
    void CombineMesh()
    {
        // Drag this into parent game obj then run this function 
        Quaternion _oldRot = transform.rotation;
        Vector3 _oldPos = transform.position;

        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;

        MeshFilter[] _filters = GetComponentsInChildren<MeshFilter>();

        Debug.Log($"{name} is combining {_filters.Length} meshes");

        Mesh _finalMesh = new Mesh();
        _finalMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        CombineInstance[] _combiners = new CombineInstance[_filters.Length];

        for (int i = 0; i < _filters.Length; i++)
        {
            if (_filters[i].transform == transform) continue;

            _combiners[i].subMeshIndex = 0;
            _combiners[i].mesh = _filters[i].sharedMesh;
            _combiners[i].transform = _filters[i].transform.localToWorldMatrix;

        }

        _finalMesh.CombineMeshes(_combiners);
        GetComponent<MeshFilter>().sharedMesh = _finalMesh;

        transform.rotation = _oldRot;
        transform.position = _oldPos;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        // save asset to asset folder
        UnityEditor.AssetDatabase.CreateAsset(_finalMesh, $"Assets/{name}-{UnityEditor.GUID.Generate()}.mesh");
        UnityEditor.AssetDatabase.SaveAssets();
    }


}

#endif
