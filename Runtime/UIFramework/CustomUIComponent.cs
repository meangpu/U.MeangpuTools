using EasyButtons;
using UnityEngine;

namespace Meangpu.UI
{
    public abstract class CustomUIComponent : MonoBehaviour
    {
        private void Awake() => Init();

        [Button]
        private void Init()
        {
            Setup();
            Configure();
        }

        public abstract void Setup();
        public abstract void Configure();

        private void OnValidate() => Init();
    }
}