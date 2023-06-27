using UnityEngine;
using System;

public class ScreenCaptureToTextureFile : MonoBehaviour
{
    [Tooltip("this need to be second cam")]
    [SerializeField] Camera _screenShotCam;
    public static event Action<Texture2D> DoGetTexture;

    public void CaptureScreen()
    {
        DoGetTexture?.Invoke(SaveCameraView(_screenShotCam));
    }

    Texture2D SaveCameraView(Camera cam)
    {
        int _width = 512;
        int _height = 512;
        RenderTexture screenTexture = new RenderTexture(_width, _height, 2);
        cam.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;
        cam.Render();
        Texture2D renderedTexture = new Texture2D(_width, _height);
        renderedTexture.ReadPixels(new Rect(0, 0, _width, _height), 0, 0);
        renderedTexture.Apply();

        RenderTexture.active = null;
        return renderedTexture;
    }

    void SaveTextureToFile(Texture2D renderedTexture)
    {
        byte[] byteArray = renderedTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/cameraCapture.png", byteArray);
    }
}
