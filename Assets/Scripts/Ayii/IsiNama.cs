using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class IsiNama : MonoBehaviour
{
    [SerializeField] private NamaPlayer namaPlayer;
    public TMP_InputField InputNama;
    public GameObject Panel;
    public UnityEvent SudahAda;

    void Start()
    {
        if(namaPlayer.PunyaNama)
        {
            Panel.gameObject.SetActive(false);
            SudahAda?.Invoke();
        }
        else
        {
            Panel.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (namaPlayer.PunyaNama)
        {
            Panel.gameObject.SetActive(false);
            SudahAda?.Invoke();
        }
        else
        {
            Panel.gameObject.SetActive(true);
        }
    }

    public void SetPlayerName()
    {
        string playerName = InputNama.text.Trim();
        if (!string.IsNullOrEmpty(playerName))
        {
            if(playerName == null)
            {
                Panel.gameObject.SetActive(true);
                namaPlayer.PunyaNama = false;
            }
            else
            {
                namaPlayer.Nama = playerName;
                namaPlayer.UpdateNama();
                namaPlayer.PunyaNama = true;
            }
        }
    }
}
