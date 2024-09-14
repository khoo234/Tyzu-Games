using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSekali : MonoBehaviour
{
    private string sudahKey = "Sudah";

    public UnityEvent mulaiEvent;
    public UnityEvent endEvent;

    [Header("Timer Settings")]
    public float timerDuration = 10f;

    [Header("Events")]
    public UnityEvent Saving;

    private float timer;

    void Start()
    {
        timer = 0f;

        if (PlayerPrefs.HasKey(sudahKey) && PlayerPrefs.GetInt(sudahKey) == 1)
        {
            endEvent?.Invoke();
        }
        else
        {
            mulaiEvent?.Invoke();
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timerDuration)
        {
            Saving?.Invoke();

            timer = 0f;
        }
    }

    public void Sudahhh()
    {
        PlayerPrefs.SetInt(sudahKey, 1);
        PlayerPrefs.Save();
    }
    public void ResetNya()
    {
        PlayerPrefs.SetInt(sudahKey, 0);
        PlayerPrefs.Save();
    }
}
