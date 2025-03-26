using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class ManageScenes : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadYourAsyncScene(sceneName));
            Time.timeScale = 1;
        }
        IEnumerator LoadYourAsyncScene(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (asyncLoad is { isDone: false })
            {
                yield return null;
            }
        }
    }
}
