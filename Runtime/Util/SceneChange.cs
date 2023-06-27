using UnityEngine;
using UnityEngine.SceneManagement;

namespace Meangpu.Util
{
    public class SceneChange : MonoBehaviour
    {
        public void GoToString(string sceneName) => SceneManager.LoadScene(sceneName);
        public void GoToIndex(int index) => SceneManager.LoadScene(index);
        public void RestartThisScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        public void QuitGame() => Application.Quit();
    }
}