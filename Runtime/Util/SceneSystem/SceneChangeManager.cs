using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using VInspector;

namespace Meangpu.Util
{
    public class SceneChangeManager : BaseMeSingleton<SceneChangeManager>
    {
        // put this inside System prefab like audio manager
        private bool _loading;
        public Action<string> WhenLoadingScene = delegate { };
        public static Action<string> WhenSceneLoaded;
        private int _waitingCount;
        public bool _preventLoadSameScene = true;

        [Button]
        public void RestartThisScene() => LoadSceneAsyncOperation(SceneManager.GetActiveScene().name);
        public void QuitGame() => Application.Quit();

        public void LoadScene(string sceneName)
        {
            if (SceneManager.GetActiveScene().name == sceneName) return;
            LoadSceneAsyncOperation(sceneName);
        }
        public void LoadScene(SOScene sceneObj) => LoadScene(sceneObj.SCENE_ID);

        void LoadSceneAsyncOperation(string sceneName)
        {
            if (_loading) return;
            _loading = true;
            _waitingCount = WhenLoadingScene.GetInvocationList().Length - 1;
            if (_waitingCount == 0) HandleReadyToLoad(sceneName);
            else WhenLoadingScene.Invoke(sceneName);
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

        public void LoadSceneAdditive(SOScene sceneToLoad)
        {
            if (SceneManager.GetSceneByName(sceneToLoad.SCENE_ID).isLoaded) return;
            SceneManager.LoadSceneAsync(sceneToLoad.SCENE_ID, LoadSceneMode.Additive);
        }

        public void UnloadScene(SOScene sceneToUnLoad)
        {
            if (!SceneManager.GetSceneByName(sceneToUnLoad.SCENE_ID).isLoaded) return;
            SceneManager.UnloadSceneAsync(sceneToUnLoad.SCENE_ID);
        }
    }
}