using UnityEngine;

public class KillAllChild : MonoBehaviour
{
    public static void KillAllChildInTransform(Transform parentTransform)
    {
        for (int i = parentTransform.childCount; i > 0; --i)
            DestroyImmediate(parentTransform.GetChild(0).gameObject);
    }

}
