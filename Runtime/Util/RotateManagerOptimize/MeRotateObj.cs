using UnityEngine;
using VInspector;

namespace Meangpu.Util
{
    public class MeRotateObj : MonoBehaviour
    {
        // learn this pattern from: [PERFECT Way to Rotate Coins | Unity Beginner Tutorial - YouTube](https://www.youtube.com/watch?v=pztDm7X5E9g)
        // note that fast editor enter play mode make onBecomeVisible not show on startup
        // and editor view prevent object from become invisible
        private void OnBecameVisible() => MeRotateManager.Instance?.Register(this);
        private void OnBecameInvisible() => MeRotateManager.Instance?.Unregister(this);
        [Button] void TestCallRegisterForFastMode() => MeRotateManager.Instance?.Register(this);
    }
}