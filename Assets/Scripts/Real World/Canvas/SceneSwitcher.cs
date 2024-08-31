using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Metode untuk mengganti scene
    public void LoadScene(string sceneName)
    {
        // Menyembunyikan cursor
        Cursor.visible = false;

        // Memuat scene yang ditentukan
        SceneManager.LoadScene(sceneName);
    }
}
