using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

namespace Meangpu.Util
{
    [RequireComponent(typeof(Camera))]
    public class ScreenShotMaker : MonoBehaviour
    {
        Camera _camera;
        [SerializeField] string _folderPath = "Assets/_Project/Screenshot/";
        [SerializeField] string _prefix = "img";
        [SerializeField] bool _useIncreaseNum = true;
        [SerializeField] int _nowNumCount;
        [SerializeField] List<GameObject> _objectList;
        [SerializeField] Transform _spawnPos;
        // learn from: [Creating An Inventory System in Unity - YouTube](https://www.youtube.com/watch?v=SGz3sbZkfkg&t=396s)

        [Button]
        void TakeImg()
        {
            if (_useIncreaseNum)
            {
                TakeScreenShotWithPath(_folderPath + _prefix + _nowNumCount + ".png");
                _nowNumCount++;
            }
            else
            {
                TakeScreenShotWithPath(_folderPath + _prefix + ".png");
            }
        }

        [Button]
        void TakeImgByObjectList()
        {
            foreach (GameObject gameObject in _objectList)
            {
                GameObject nowObj = Instantiate(gameObject, _spawnPos);
                TakeScreenShotWithPath(_folderPath + gameObject.name + ".png");
                if (Application.isEditor) DestroyImmediate(nowObj);
                else Destroy(nowObj);
            }
        }

        [Button]
        void TakeScreenShotWithPath(string fullPath)
        {
            if (_camera == null) _camera = GetComponent<Camera>();
            RenderTexture rt = new(256, 256, 24);
            _camera.targetTexture = rt;
            Texture2D screenShot = new(256, 256, TextureFormat.RGBA32, false);
            _camera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
            _camera.targetTexture = null;
            RenderTexture.active = null;

            if (Application.isEditor) DestroyImmediate(rt);
            else Destroy(rt);

            byte[] bytes = screenShot.EncodeToPNG();
            System.IO.File.WriteAllBytes(fullPath, bytes);
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
    }
}