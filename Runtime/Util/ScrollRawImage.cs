using UnityEngine;
using UnityEngine.UI;

namespace Meangpu.Util
{
    // from Tarodev [Scrolling Background in 90 seconds - Unity Tutorial - YouTube](https://www.youtube.com/watch?v=-6H-uYh80vc) he is the best guy ever! 
    public class ScrollRawImage : MonoBehaviour
    {
        [Tooltip("image need wrap mode to be repeat")]
        [SerializeField] RawImage _img;
        [SerializeField] Vector2 _scrollDir;

        private void FixedUpdate() => _img.uvRect = new Rect(_img.uvRect.position + (_scrollDir * Time.fixedDeltaTime), _img.uvRect.size);
    }
}
