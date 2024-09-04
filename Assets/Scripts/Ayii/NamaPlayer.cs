using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NamaPlayer : MonoBehaviour
{
    public string Nama;
    public TMP_Text namaText;
    public bool PunyaNama;

    private const string NamaKey = "PlayerName";
    private const string PunyaNamaKey = "PunyaNama";

    void Start()
    {
        if (PlayerPrefs.HasKey(NamaKey))
        {
            Nama = PlayerPrefs.GetString(NamaKey);
        }

        if (PlayerPrefs.HasKey(PunyaNamaKey))
        {
            PunyaNama = PlayerPrefs.GetInt(PunyaNamaKey) == 1;
        }

        namaText.text = Nama;
    }

    private void Update()
    {
        namaText.text = Nama;
    }

    public void SimpanNama()
    {
        PlayerPrefs.SetString(NamaKey, Nama);
        PlayerPrefs.SetInt(PunyaNamaKey, 1);
        PlayerPrefs.Save();
    }

    public void ResetNama()
    {
        PlayerPrefs.SetInt(PunyaNamaKey, 0);
        PunyaNama = false;
    }

    public void UpdateNama()
    {
        namaText.text = Nama;
        SimpanNama();
    }
}
