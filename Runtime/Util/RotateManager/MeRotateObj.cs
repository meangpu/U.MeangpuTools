using UnityEngine;

namespace Meangpu.Util
{
    public class MeRotateObj : MonoBehaviour
    {
        private void OnBecameVisible() => MeRotateManager.Instance.Register(this);
        private void OnBecameInvisible() => MeRotateManager.Instance.Unregister(this);
    }
}