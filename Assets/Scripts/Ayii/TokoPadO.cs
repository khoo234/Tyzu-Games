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

    void Start()
    {
        ScriptUpiah = FindAnyObjectByType<UpiahManager>();
        InfoHarga1.text = "Rp. " + HargaTnmn;
        InfoHarga2.text = "Rp. " + HargaLmr;
        InfoHarga3.text = "Rp. " + HargaVbg;
        InfoHarga4.text = "Rp. " + HargaKrt1;
        InfoHarga5.text = "Rp. " + HargaKrt2;
        InfoHarga6.text = "Rp. " + HargaMbl;
    }

    void Update()
    {
        InfoRupiah.text = ScriptUpiah.totalUpiah.ToString();
    }

    public void Beli1()
    {
        Tanamn = true;
        if (ScriptUpiah.totalUpiah > HargaTnmn)
        {
            ScriptUpiah.UpiahKurang(HargaTnmn);
            Tanaman.gameObject.SetActive(true);
            BeliTnmn.interactable = false;
            BeliTnmn2.interactable = false;
            BerhasilDibeli.ShowPopup();
        }
        else
        {
            UpiahTdkCkp.ShowPopup();
        }
    }
    public void Beli2()
    {
        if (ScriptUpiah.totalUpiah > HargaLmr)
        {
            Lmri = true;
            ScriptUpiah.UpiahKurang(HargaLmr);
            Lemari.gameObject.SetActive(true);
            BeliLmr.interactable = false;
            BeliLmr2.interactable = false;
            BerhasilDibeli.ShowPopup();
        }
        else
        {
            UpiahTdkCkp.ShowPopup();
        }
    }
    public void Beli3()
    {
        if (ScriptUpiah.totalUpiah > HargaVbg)
        {
            Vasbng = true;
            ScriptUpiah.UpiahKurang(HargaVbg);
            VasBunga.gameObject.SetActive(true);
            BeliVbg.interactable = false;
            BeliVbg2.interactable = false;
            BerhasilDibeli.ShowPopup();
        }
        else
        {
            UpiahTdkCkp.ShowPopup();
        }
    }
    public void Beli4()
    {
        if (ScriptUpiah.totalUpiah > HargaKrt1)
        {
            Karpt1 = true;
            ScriptUpiah.UpiahKurang(HargaKrt1);
            Karpet1.gameObject.SetActive(true);
            BeliKrt1.interactable = false;
            BeliKrt12.interactable = false;
            BerhasilDibeli.ShowPopup();
        }
        else
        {
            UpiahTdkCkp.ShowPopup();
        }
    }
    public void Beli5()
    {
        if (ScriptUpiah.totalUpiah > HargaKrt2)
        {
            Karpt2 = true;
            ScriptUpiah.UpiahKurang(HargaKrt2);
            Karpet2.gameObject.SetActive(true);
            BeliKrt2.interactable = false;
            BeliKrt22.interactable = false;
            BerhasilDibeli.ShowPopup();
        }
        else
        {
            UpiahTdkCkp.ShowPopup();
        }
    }
    public void Beli6()
    {
        if (ScriptUpiah.totalUpiah > HargaMbl)
        {
            Mobl = true;
            ScriptUpiah.UpiahKurang(HargaMbl);
            Mobil.gameObject.SetActive(true);
            BeliMbl.interactable = false;
            BeliMbl2.interactable = false;
            BerhasilDibeli.ShowPopup();
        }
        else
        {
            UpiahTdkCkp.ShowPopup();
        }
    }
}
