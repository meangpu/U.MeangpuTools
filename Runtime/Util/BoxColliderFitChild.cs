using UnityEngine;
using VInspector;

namespace Meangpu.Util
{
    public class BoxColliderFitChild : MonoBehaviour
    {
        [SerializeField] GameObject parentGameobject;

        [Button]
        private void FitColliderToChildren()
        {
            if (parentGameobject == null) parentGameobject = gameObject;

            BoxCollider bc = GetOrCreate.GetCreateComponent<BoxCollider>(parentGameobject);

            Bounds bounds = new(Vector3.zero, Vector3.zero);
            bool hasBounds = false;
            foreach (Renderer render in parentGameobject.GetComponentsInChildren<Renderer>())
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
                bc.center = bounds.center - parentGameobject.transform.position;
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
