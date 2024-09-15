using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TokoPadO : MonoBehaviour
{
    private UpiahManager ScriptUpiah;

    [Header("Info Upiah")]
    public TMP_Text InfoRupiah;

    [Header("Tanaman")]
    public int HargaTnmn;
    public GameObject Tanaman;
    public Button BeliTnmn;
    public Button BeliTnmn2;
    public TMP_Text InfoHarga1;
    public bool Tanamn;

    [Header("Lemari")]
    public int HargaLmr;
    public bool Lmri;
    public GameObject Lemari;
    public Button BeliLmr;
    public Button BeliLmr2;
    public TMP_Text InfoHarga2;

    [Header("Vas Bunga")]
    public int HargaVbg;
    public GameObject VasBunga;
    public Button BeliVbg;
    public Button BeliVbg2;
    public TMP_Text InfoHarga3;
    public bool Vasbng;

    [Header("Karpet 1")]
    public int HargaKrt1;
    public GameObject Karpet1;
    public Button BeliKrt1;
    public Button BeliKrt12;
    public TMP_Text InfoHarga4;
    public bool Karpt1;

    [Header("Karpet 2")]
    public int HargaKrt2;
    public GameObject Karpet2;
    public Button BeliKrt2;
    public Button BeliKrt22;
    public TMP_Text InfoHarga5;
    public bool Karpt2;

    [Header("Mobil")]
    public int HargaMbl;
    public GameObject Mobil;
    public Button BeliMbl;
    public Button BeliMbl2;
    public TMP_Text InfoHarga6;
    public bool Mobl;

    [Header("Animasi")]
    public Notifikasi UpiahTdkCkp;
    public Notifikasi BerhasilDibeli;
    public AudioSource Buy;
    public AudioSource NotEnough;
    void Start()
    {
        ScriptUpiah = FindAnyObjectByType<UpiahManager>();
        InfoHarga1.text = "Rp. " + HargaTnmn;
        InfoHarga2.text = "Rp. " + HargaLmr;
        InfoHarga3.text = "Rp. " + HargaVbg;
        InfoHarga4.text = "Rp. " + HargaKrt1;
        InfoHarga5.text = "Rp. " + HargaKrt2;
        InfoHarga6.text = "Rp. " + HargaMbl;

        LoadData();
    }

    void Update()
    {
        InfoRupiah.text = ScriptUpiah.totalUpiah.ToString();

        BeliTnmn.interactable = !Tanamn;
        BeliTnmn2.interactable = !Tanamn;
        BeliLmr.interactable = !Lmri;
        BeliLmr2.interactable = !Lmri;
        BeliVbg.interactable = !Vasbng;
        BeliVbg2.interactable = !Vasbng;
        BeliKrt1.interactable = !Karpt1;
        BeliKrt12.interactable = !Karpt1;
        BeliKrt2.interactable = !Karpt2;
        BeliKrt22.interactable = !Karpt2;
        BeliMbl.interactable = !Mobl;
        BeliMbl2.interactable = !Mobl;
    }

    public void Beli1()
    {
        if (ScriptUpiah.totalUpiah >= HargaTnmn)
        {
            Tanamn = true;
            ScriptUpiah.UpiahKurang(HargaTnmn);
            Tanaman.gameObject.SetActive(true);
            BerhasilDibeli.ShowPopup();
            SaveData();
            Buy.Play();
        }
        else
        {
            NotEnough.Play();
            UpiahTdkCkp.ShowPopup();
        }
    }

    public void Beli2()
    {
        if (ScriptUpiah.totalUpiah >= HargaLmr)
        {
            Lmri = true;
            ScriptUpiah.UpiahKurang(HargaLmr);
            Lemari.gameObject.SetActive(true);
            BerhasilDibeli.ShowPopup();
            SaveData();
            Buy.Play();
        }
        else
        {
            NotEnough.Play();
            UpiahTdkCkp.ShowPopup();
        }
    }

    public void Beli3()
    {
        if (ScriptUpiah.totalUpiah >= HargaVbg)
        {
            Vasbng = true;
            ScriptUpiah.UpiahKurang(HargaVbg);
            VasBunga.gameObject.SetActive(true);
            BerhasilDibeli.ShowPopup();
            SaveData();
            Buy.Play();
        }
        else
        {
            NotEnough.Play();
            UpiahTdkCkp.ShowPopup();
        }
    }

    public void Beli4()
    {
        if (ScriptUpiah.totalUpiah >= HargaKrt1)
        {
            Karpt1 = true;
            ScriptUpiah.UpiahKurang(HargaKrt1);
            Karpet1.gameObject.SetActive(true);
            BerhasilDibeli.ShowPopup();
            SaveData();
            Buy.Play();
        }
        else
        {
            NotEnough.Play();
            UpiahTdkCkp.ShowPopup();
        }
    }

    public void Beli5()
    {
        if (ScriptUpiah.totalUpiah >= HargaKrt2)
        {
            Karpt2 = true;
            ScriptUpiah.UpiahKurang(HargaKrt2);
            Karpet2.gameObject.SetActive(true);
            BerhasilDibeli.ShowPopup();
            SaveData();
            Buy.Play();
        }
        else
        {
            NotEnough.Play();
            UpiahTdkCkp.ShowPopup();
        }
    }

    public void Beli6()
    {
        if (ScriptUpiah.totalUpiah >= HargaMbl)
        {
            Mobl = true;
            ScriptUpiah.UpiahKurang(HargaMbl);
            Mobil.gameObject.SetActive(true);
            BerhasilDibeli.ShowPopup();
            SaveData();
            Buy.Play();
        }
        else
        {
            NotEnough.Play();
            UpiahTdkCkp.ShowPopup();
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("Tanamn", Tanamn ? 1 : 0);
        PlayerPrefs.SetInt("Lmri", Lmri ? 1 : 0);
        PlayerPrefs.SetInt("Vasbng", Vasbng ? 1 : 0);
        PlayerPrefs.SetInt("Karpt1", Karpt1 ? 1 : 0);
        PlayerPrefs.SetInt("Karpt2", Karpt2 ? 1 : 0);
        PlayerPrefs.SetInt("Mobl", Mobl ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        Tanamn = PlayerPrefs.GetInt("Tanamn", 0) == 1;
        Lmri = PlayerPrefs.GetInt("Lmri", 0) == 1;
        Vasbng = PlayerPrefs.GetInt("Vasbng", 0) == 1;
        Karpt1 = PlayerPrefs.GetInt("Karpt1", 0) == 1;
        Karpt2 = PlayerPrefs.GetInt("Karpt2", 0) == 1;
        Mobl = PlayerPrefs.GetInt("Mobl", 0) == 1;

        Tanaman.SetActive(Tanamn);
        Lemari.SetActive(Lmri);
        VasBunga.SetActive(Vasbng);
        Karpet1.SetActive(Karpt1);
        Karpet2.SetActive(Karpt2);
        Mobil.SetActive(Mobl);
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteKey("Tanamn");
        PlayerPrefs.DeleteKey("Lmri");
        PlayerPrefs.DeleteKey("Vasbng");
        PlayerPrefs.DeleteKey("Karpt1");
        PlayerPrefs.DeleteKey("Karpt2");
        PlayerPrefs.DeleteKey("Mobl");
    }
}
