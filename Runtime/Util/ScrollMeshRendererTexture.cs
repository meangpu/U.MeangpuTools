using UnityEngine;

namespace Meangpu.Util
{
    public class ScrollMeshRendererTexture : MonoBehaviour
    {
        [SerializeField] Vector2 _scrollSpeed;
        [SerializeField] MeshRenderer _renderer;

        private void FixedUpdate() => _renderer.material.mainTextureOffset = new(Time.realtimeSinceStartup * _scrollSpeed.x, Time.realtimeSinceStartup * _scrollSpeed.y);

        public void UpdateScrollSpeed(Vector2 newSpeed) => _scrollSpeed = newSpeed;
        public void AddToScrollSpeed(Vector2 addedSpeed) => _scrollSpeed += addedSpeed;

    }
}
