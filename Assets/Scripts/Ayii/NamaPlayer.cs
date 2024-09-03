using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NamaPlayer : MonoBehaviour
{
    public string Nama;
    public TMP_Text namaText;

    private const string NamaKey = "PlayerName";

    void Start()
    {
        if (PlayerPrefs.HasKey(NamaKey))
        {
            Nama = PlayerPrefs.GetString(NamaKey);
        }

        if (namaText != null)
        {
            namaText.text = Nama;
        }
    }

    public void SimpanNama()
    {
        PlayerPrefs.SetString(NamaKey, Nama);
        PlayerPrefs.Save();
    }

    public void UpdateNama()
    {
        namaText.text = Nama;
        SimpanNama();
    }
}
