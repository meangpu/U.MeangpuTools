using UnityEngine;

namespace Meangpu.Util
{
    public class DisableMeOnStartPlayMode : MonoBehaviour
    {
        private void Awake() => gameObject.SetActive(false);
    }
}