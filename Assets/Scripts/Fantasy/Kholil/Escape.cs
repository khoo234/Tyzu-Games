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
            Cursor.visible = false;
            PlayerPrefs.SetInt("TotalCoins", GameManager.Instance.GetTotalCoins());
            Pause = true;
            Aktif?.Invoke();
            StartCoroutine(Delay());
            KursorMuncul();
        }
        else if(Pause && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Pause = false;
            Mati?.Invoke();
            KursorHilang();
            Time.timeScale = 1f;
            StopCoroutine(Delay());
        }
    }

    private IEnumerator Delay ()
    {
        yield return new WaitForSecondsRealtime(0.5f);

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
        StopCoroutine(Delay());
        Mati?.Invoke();
    }

    public void KursorMuncul()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
