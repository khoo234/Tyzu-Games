using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneClick : MonoBehaviour
{
    [Header("Main Settings")]
    public string TargetScene;
    public float delay = 1f;

    public void LoadScene()
    {
        StartCoroutine(LoadSceneWithDelay());
    }

    private IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(delay);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(TargetScene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
