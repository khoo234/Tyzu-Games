using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    

    // Metode untuk mengganti scene
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

   
}
