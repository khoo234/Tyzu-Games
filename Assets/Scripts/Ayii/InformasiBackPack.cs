using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InformasiBackPack : MonoBehaviour
{
    [Header("Menampilkan")]
    public TMP_Text Judul;
    public TMP_Text Informasi;

    [Header("Item 1")]
    [SerializeField] private string Judul1;
    [SerializeField] private string Informasi1;
    [SerializeField] private GameObject Gambar1;

    [Header("Item 2")]
    [SerializeField] private string Judul2;
    [SerializeField] private string Informasi2;
    [SerializeField] private GameObject Gambar2;

    [Header("Item 3")]
    [SerializeField] private string Judul3;
    [SerializeField] private string Informasi3;
    [SerializeField] private GameObject Gambar3;

    [Header("Item 4")]
    [SerializeField] private string Judul4;
    [SerializeField] private string Informasi4;
    [SerializeField] private GameObject Gambar4;

    [Header("Item 5")]
    [SerializeField] private string Judul5;
    [SerializeField] private string Informasi5;
    [SerializeField] private GameObject Gambar5;

    [Header("Item 6")]
    [SerializeField] private string Judul6;
    [SerializeField] private string Informasi6;
    [SerializeField] private GameObject Gambar6;

    [Header("Item 7")]
    [SerializeField] private string Judul7;
    [SerializeField] private string Informasi7;
    [SerializeField] private GameObject Gambar7;

    [Header("Item 8")]
    [SerializeField] private string Judul8;
    [SerializeField] private string Informasi8;
    [SerializeField] private GameObject Gambar8;

    [Header("Item 9")]
    [SerializeField] private string Judul9;
    [SerializeField] private string Informasi9;
    [SerializeField] private GameObject Gambar9;

    private void Start()
    {
        Item1(); // Default to Item1 at the start
    }

    public void Item1()
    {
        Judul.text = Judul1;
        Informasi.text = Informasi1;
        Gambar1.SetActive(true);
        DeactivateOtherImages(Gambar1);
    }

    public void Item2()
    {
        Judul.text = Judul2;
        Informasi.text = Informasi2;
        Gambar2.SetActive(true);
        DeactivateOtherImages(Gambar2);
    }

    public void Item3()
    {
        Judul.text = Judul3;
        Informasi.text = Informasi3;
        Gambar3.SetActive(true);
        DeactivateOtherImages(Gambar3);
    }

    public void Item4()
    {
        Judul.text = Judul4;
        Informasi.text = Informasi4;
        Gambar4.SetActive(true);
        DeactivateOtherImages(Gambar4);
    }

    public void Item5()
    {
        Judul.text = Judul5;
        Informasi.text = Informasi5;
        Gambar5.SetActive(true);
        DeactivateOtherImages(Gambar5);
    }

    public void Item6()
    {
        Judul.text = Judul6;
        Informasi.text = Informasi6;
        Gambar6.SetActive(true);
        DeactivateOtherImages(Gambar6);
    }

    public void Item7()
    {
        Judul.text = Judul7;
        Informasi.text = Informasi7;
        Gambar7.SetActive(true);
        DeactivateOtherImages(Gambar7);
    }

    public void Item8()
    {
        Judul.text = Judul8;
        Informasi.text = Informasi8;
        Gambar8.SetActive(true);
        DeactivateOtherImages(Gambar8);
    }

    public void Item9()
    {
        Judul.text = Judul9;
        Informasi.text = Informasi9;
        Gambar9.SetActive(true);
        DeactivateOtherImages(Gambar9);
    }

    private void DeactivateOtherImages(GameObject activeImage)
    {
        GameObject[] allImages = { Gambar1, Gambar2, Gambar3, Gambar4, Gambar5, Gambar6, Gambar7, Gambar8, Gambar9 };
        foreach (GameObject img in allImages)
        {
            if (img != activeImage)
            {
                img.SetActive(false);
            }
        }
    }
}
