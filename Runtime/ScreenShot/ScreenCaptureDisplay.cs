using UnityEngine;
using UnityEngine.UI;

public class ScreenCaptureDisplay : MonoBehaviour
{
    [SerializeField] RawImage _img;

    void OnEnable()
    {
        ScreenCaptureToTextureFile.DoGetTexture += SetPaperTexture;
    }

    void OnDisable()
    {
        ScreenCaptureToTextureFile.DoGetTexture -= SetPaperTexture;
    }

    public Sprite ConvertToSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }

    void Start()
    {
        _img.enabled = false;
    }


    void SetPaperTexture(Texture2D _tex)
    {
        _img.enabled = true;
        _img.texture = _tex;
    }

}
