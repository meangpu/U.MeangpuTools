using UnityEngine;
using VInspector;

namespace Meangpu.Util
{
    public class BoxColliderFitChild : MonoBehaviour
    {
        [SerializeField] GameObject _parentTrans;

        [Button]
        private void FitColliderToChildren()
        {
            BoxCollider bc = _parentTrans.GetComponent<BoxCollider>() ?? _parentTrans.AddComponent<BoxCollider>();
            Bounds bounds = new(Vector3.zero, Vector3.zero);
            bool hasBounds = false;
            foreach (Renderer render in _parentTrans.GetComponentsInChildren<Renderer>())
            {
                if (hasBounds)
                {
                    bounds.Encapsulate(render.bounds);
                }
                else
                {
                    bounds = render.bounds;
                    hasBounds = true;
                }
            }
            if (hasBounds)
            {
                bc.center = bounds.center - _parentTrans.transform.position;
                bc.size = bounds.size;
            }
            else
            {
                bc.size = bc.center = Vector3.zero;
                bc.size = Vector3.zero;
            }
        }
    }
}
