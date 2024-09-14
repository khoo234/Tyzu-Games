using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;
public class Escape : MonoBehaviour
{
    [Header("UI")]
    public bool Pause;

    [Header("Events")]
    public UnityEvent Aktif;
    public UnityEvent Mati;

    void Update()
    {
        if (!Pause)
        {
            Time.timeScale = 1f;
        }
        if (!Pause && Input.GetKeyDown(KeyCode.Escape))
        {
            KursorMuncul();
        }
        else if(Pause && Input.GetKeyDown(KeyCode.Escape))
        {
            KursorHilang();
        }
        PlayerPrefs.SetInt("TotalCoins", GameManager.Instance.GetTotalCoins());
    }

    private IEnumerator Delay ()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        Time.timeScale = 0;
    }

    public void GantiScene(string NamaScene)
    {
        SceneManager.LoadScene(NamaScene);
    }

    public void KursorHilang()
    {
        Pause = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        Mati?.Invoke();
        StopCoroutine(Delay());
    }

    public void KursorMuncul()
    {
        Pause = true;
        Aktif?.Invoke();
        StartCoroutine(Delay());
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
