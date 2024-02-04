using UnityEngine;
using VInspector;

namespace Meangpu.Util
{
    public class AddColliderToChildMeshRenderer : MonoBehaviour
    {
        [Button] public void AddCollider() => AddCollidersRecursively(transform);

        void AddCollidersRecursively(Transform parent)
        {
            int count = 0;
            foreach (Transform child in parent)
            {
                if (child.TryGetComponent<MeshRenderer>(out _))
                {
                    if (child.GetComponent<BoxCollider>() == null)
                    {
                        child.gameObject.AddComponent<BoxCollider>();
                        Debug.Log("Add BoxCollider to " + child.name);
                        count++;
                    }
                }
                AddCollidersRecursively(child);
            }
            Debug.Log("=======SUMMARY=======");
            Debug.Log($"add <b><color=lightblue>{count}</color></b> collider to {parent.name}");
            Debug.Log("=======SUMMARY=======");
        }
    }
}