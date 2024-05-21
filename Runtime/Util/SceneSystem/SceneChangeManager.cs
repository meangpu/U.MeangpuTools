using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using UnityEngine.UI;
using VInspector;
using System.Threading.Tasks;

namespace Meangpu.Util
{
    public class SceneChangeManager : BaseMeSingleton<SceneChangeManager>
    {
        // put this inside System prefab like audio manager
        private bool _loading;
        public Action<string> WhenLoadingScene = delegate { };
        public static Action<string> WhenSceneLoaded;
        private int _waitingCount;

        public void RestartThisScene() => LoadScene(SceneManager.GetActiveScene().name);
        public void QuitGame() => Application.Quit();

        [SerializeField] bool _isUseLoaderCanvas;
        [ShowIf("_isUseLoaderCanvas")]
        [SerializeField] GameObject _loaderCanvas;
        [SerializeField] Slider _progressBar;
        float _targetLoading;
        [EndIf]

        private void Start()
        {
            if (_progressBar != null)
            {
                _progressBar.maxValue = 1;
                _progressBar.minValue = 0;
                _progressBar.value = 0;
            }
        }

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
            if (_isUseLoaderCanvas)
            {
                _progressBar.value = 0;
                _loaderCanvas.SetActive(true);
                do
                {
                    _targetLoading = asyncLoad.progress;
                    yield return null;
                }
                while (asyncLoad.progress < .9f);
                _loaderCanvas.SetActive(false);

                WhenSceneLoaded?.Invoke(sceneName);
                _loading = false;
            }
            else
            {
                while (!asyncLoad.isDone) yield return null;
                WhenSceneLoaded?.Invoke(sceneName);
                _loading = false;
            }
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

        private void Update()
        {
            if (!_loading) return;
            _progressBar.value = Mathf.MoveTowards(_progressBar.value, _targetLoading, 3 * Time.deltaTime);
        }
    }
}