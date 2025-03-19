using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadYourAsyncScene(sceneName));
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
