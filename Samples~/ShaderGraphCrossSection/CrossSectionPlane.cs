using UnityEngine;

namespace Meangpu.Shader
{
    [ExecuteInEditMode]
    public class CrossSectionPlane : MonoBehaviour
    {
        // learn from this cool guy from internet [Unity3d Cross Section Shader Using Shader Graph | by Abdullah Aldandarawy | codeburst](https://codeburst.io/unity-cross-section-shader-using-shader-graph-31c3fed0fa4f)
        public Material mat1, mat2;
        [SerializeField] Transform _targetOffset;
        [SerializeField] Vector3 _defaultOffsetPos = new(0, 1, 0);
        void Update()
        {
            mat1.SetVector("_PlanePosition", GetPositionOffset());
            mat1.SetVector("_PlaneNormal", transform.up);
            mat2.SetVector("_PlanePosition", GetPositionOffset());
            mat2.SetVector("_PlaneNormal", transform.up);
        }

        Vector3 GetPositionOffset() => transform.localPosition - _targetOffset.localPosition - _defaultOffsetPos;


    }
}