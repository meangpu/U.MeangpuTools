using UnityEngine;
using VInspector;

namespace Meangpu.Util
{
    public class KillChildCollider : MonoBehaviour
    {
        [SerializeField] Transform _parentTrans;

        [Button]
        void KillAllChildCollider()
        {
            if (_parentTrans == null) _parentTrans = transform;
            foreach (var c in _parentTrans.GetComponentsInChildren<Collider>(true)) DoDestroyComponent(c);
        }

        [Button]
        void KillAllChildRigidbody()
        {
            if (_parentTrans == null) _parentTrans = transform;
            foreach (var c in _parentTrans.GetComponentsInChildren<Rigidbody>(true)) DoDestroyComponent(c);
        }

        void DoDestroyComponent(Component c)
        {
            if (c)
            {
                Debug.Log($"Destroying... {c} from {c.name}");
                DestroyImmediate(c);
            }
        }
    }
}