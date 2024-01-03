using UnityEngine;
using VInspector;

namespace Meangpu.Util
{
    public class KillChildCollider : MonoBehaviour
    {
        [SerializeField] Transform _parentTrans;
        [SerializeField] bool _includeInactive = true;

        [Button]
        void KillAllChildCollider()
        {
            if (_parentTrans == null) _parentTrans = transform;
            foreach (var c in _parentTrans.GetComponentsInChildren<Collider>(_includeInactive)) DoDestroyComponent(c);
        }

        [Button]
        void KillAllChildRigidbody()
        {
            if (_parentTrans == null) _parentTrans = transform;
            foreach (var c in _parentTrans.GetComponentsInChildren<Rigidbody>(_includeInactive)) DoDestroyComponent(c);
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