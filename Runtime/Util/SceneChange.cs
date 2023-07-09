using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

namespace Meangpu.Util
{
    public class SceneChange : MonoBehaviour
    {
        private bool _loading;
        public Action<string> WhenLoadingScene = delegate { };
        public Action<string> WhenSceneLoaded = delegate { };
        private int _waitingCount;

        public void GoToString(string sceneName) => SceneManager.LoadScene(sceneName);
        public void GoToIndex(int index) => SceneManager.LoadScene(index);
        public void RestartThisScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        public void QuitGame() => Application.Quit();

        public void LoadScene(string sceneName)
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
            WhenSceneLoaded.Invoke(sceneName);
        }
    }
}