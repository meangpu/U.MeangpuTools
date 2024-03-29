using UnityEngine;
using Meangpu.Pool;

namespace Meangpu.Util
{
    public class KillAllChild : MonoBehaviour
    {
        public static void KillAllChildInTransform(Transform parentTransform)
        {
            for (int i = parentTransform.childCount; i > 0; --i)
                DestroyImmediate(parentTransform.GetChild(0).gameObject);
        }

        public static void KillAllChildPool(Transform parentTransform)
        {
            foreach (Transform child in parentTransform) PoolManager.ReturnObjectToPool(child.gameObject);
        }

        public static void KillAllChildInTransformDestroyNormal(Transform parentTransform)
        {
            foreach (Transform child in parentTransform) Destroy(child.gameObject);
        }

        public static void KillWithTag(Transform parentTransform, string tag)
        {
            // compare children of game object
            for (var i = parentTransform.childCount - 1; i >= 0; i--)
            {
                // only destroy tagged object
                if (parentTransform.GetChild(i).gameObject.CompareTag(tag))
                    DestroyImmediate(parentTransform.GetChild(i).gameObject);
            }
        }
    }
}