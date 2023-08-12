using UnityEditor;
using UnityEngine;

namespace Meangpu
{
    public class MeshCombiner : EditorWindow
    {
        public Transform TransformToCombine;

        [MenuItem("MeangpuTools/MeshCombine")]
        public static void ShowWindow()
        {
            GetWindow<MeshCombiner>("MeshCombiner");
        }

        private void OnGUI()
        {
            TransformToCombine = EditorGUILayout.ObjectField("TransformToCombine", TransformToCombine, typeof(Transform), true) as Transform;

            EditorGUILayout.LabelField("Count", EditorStyles.boldLabel);

            EditorGUILayout.Space();
            EditorGUILayout.Separator();

            if (GUILayout.Button("Combine Mesh")) CombineMesh();
        }

        void CombineMesh()
        {
            // this pass horse model test with 839 batch to only 8 batch, and fps from lag 4.2ms to 1.8 ms on empty scene
            if (TransformToCombine == null)
            {
                Debug.Log($"<color=red>Parent mesh transform is null!</color>");
                return;
            }

            // Drag this into parent game obj then run this function 
            Quaternion _oldRot = TransformToCombine.rotation;
            Vector3 _oldPos = TransformToCombine.position;

            TransformToCombine.rotation = Quaternion.identity;
            TransformToCombine.position = Vector3.zero;

            MeshFilter[] _filters = TransformToCombine.GetComponentsInChildren<MeshFilter>();

            Debug.Log($"{name} is combining {_filters.Length} meshes");

            Mesh _finalMesh = new()
            {
                indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
            };

            CombineInstance[] _combiners = new CombineInstance[_filters.Length];

            for (int i = 0; i < _filters.Length; i++)
            {
                if (_filters[i].transform == TransformToCombine) continue;

                _combiners[i].subMeshIndex = 0;
                _combiners[i].mesh = _filters[i].sharedMesh;
                _combiners[i].transform = _filters[i].transform.localToWorldMatrix;
            }

            _finalMesh.CombineMeshes(_combiners);
            GetCreateComponent<MeshFilter>(TransformToCombine).sharedMesh = _finalMesh;

            TransformToCombine.rotation = _oldRot;
            TransformToCombine.position = _oldPos;

            for (int i = 0; i < TransformToCombine.childCount; i++)
            {
                TransformToCombine.GetChild(i).gameObject.SetActive(false);
            }

            // save asset to asset folder
            SaveMeshAsset(_finalMesh);
            Debug.Log($"<color=#4ec9b0>Finish Create obj</color>");
        }

        private void SaveMeshAsset(Mesh _finalMesh)
        {
            string assetPath = $"Assets/{name}-{GUID.Generate()}.mesh";

            AssetDatabase.CreateAsset(_finalMesh, assetPath);
            AssetDatabase.SaveAssets();

            Selection.activeObject = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Object));
        }

        public static T GetCreateComponent<T>(Transform _transform) where T : Component
        {
            if (_transform.GetComponent<T>()) return _transform.GetComponent<T>();
            return _transform.gameObject.AddComponent<T>();
        }
    }
}
