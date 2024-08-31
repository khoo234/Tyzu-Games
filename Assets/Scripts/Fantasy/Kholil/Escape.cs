using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    public string targetSceneName = "realworld";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = false;
            // Simpan data koin sebelum pindah scene
            PlayerPrefs.SetInt("TotalCoins", GameManager.Instance.GetTotalCoins());
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
