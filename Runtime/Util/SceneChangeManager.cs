using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

namespace Meangpu.Util
{
    public class SceneChangeManager : MonoBehaviour
    {
        // put this inside System prefab like audio manager

        public static SceneChangeManager instance;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);
        }

        private bool _loading;
        public Action<string> WhenLoadingScene = delegate { };
        public static Action<string> WhenSceneLoaded;
        private int _waitingCount;

        public void RestartThisScene() => LoadScene(SceneManager.GetActiveScene().name);
        public void QuitGame() => Application.Quit();

        public void LoadScene(string sceneName)
        {
            if (_loading) return;
            _loading = true;
            _waitingCount = WhenLoadingScene.GetInvocationList().Length - 1;
            if (_waitingCount == 0) HandleReadyToLoad(sceneName);
            else WhenLoadingScene.Invoke(sceneName);
        }

        public void LoadScene(SOScene sceneObj)
        {
            if (_loading) return;
            _loading = true;
            _waitingCount = WhenLoadingScene.GetInvocationList().Length - 1;
            if (_waitingCount == 0) HandleReadyToLoad(sceneObj.SCENE_ID);
            else WhenLoadingScene.Invoke(sceneObj.SCENE_ID);
        }

        public void HandleReadyToLoad(string sceneName)
        {
            _waitingCount--;
            if (_waitingCount <= 0) StartCoroutine(LoadSceneAsync(sceneName));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncLoad.isDone) yield return null;
            WhenSceneLoaded?.Invoke(sceneName);
            _loading = false;
        }
    }
}