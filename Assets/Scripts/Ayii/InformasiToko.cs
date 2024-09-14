using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InformasiToko : MonoBehaviour
{
    [Header ("Menampilkan")]
    public TMP_Text Judul;
    public TMP_Text Informasi;

    [Header ("Item 1")]
    [SerializeField] private string Judul1;
    [SerializeField] private string Informasi1;
    [SerializeField] private GameObject Gambar1;
    [SerializeField] private GameObject JualItem1;

    [Header ("Item 2")]
    [SerializeField] private string Judul2;
    [SerializeField] private string Informasi2;
    [SerializeField] private GameObject Gambar2;
    [SerializeField] private GameObject JualItem2;

    private void Start()
    {
        Item1();
    }

    public void Item1()
    {
        Judul.text = Judul1;
        Informasi.text = Informasi1;
        Gambar1.SetActive (true);
        JualItem1.SetActive (true);
        JualItem2.SetActive (false);
        Gambar2.SetActive (false);
    }
    public void Item2()
    {
        Judul.text = Judul2;
        Informasi.text = Informasi2;
        Gambar2.SetActive(true);
        JualItem2.SetActive(true);
        JualItem1.SetActive(false);
        Gambar1.SetActive(false);
    }
}
