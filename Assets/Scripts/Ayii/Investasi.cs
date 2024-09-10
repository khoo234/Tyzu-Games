using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Investasi : MonoBehaviour
{
    private UpiahManager Script;
    public Notifikasi NotifTdkCukup;
    public AnimasiPosisi NotifKeuntungan;
    public Notifikasi NotifMasukkanUpiah;
    public Notifikasi BerhasilDibeli;

    [Header("Info Keuntungan")]
    public TMP_Text AngkaKeuntungan;
    public TMP_Text Rupiah;

    [Header("Invest Jangka Pendek")]
    public TMP_InputField JumlahJangkaPendek;
    public int InvestJangkaPendek;
    public float totalPendapatanJangkaPendek;
    private bool isInvestingJPK;

    [Header("Invest Jangka Panjang")]
    public TMP_InputField JumlahJangkaPanjang;
    public int InvestJangkaPanjang;
    public float totalPendapatanJangkaPanjang;

    [Header ("Persen Keuntungan (Gunakan 0.1 = 1%)")]
    public float PersenKeuntunganJPK;
    public float PersenKeuntunganJPG;
    private bool isInvestingJPG;

    [Header("Info Waktu")]
    public TMP_Text WaktuTextJangkaPendek;
    public TMP_Text WaktuTextJangkaPanjang;

    [Header("Waktu Jangka")]
    public int WaktuJangkaPendek;
    public int WaktuJangkaPanjang;

    [Header("Tombol Jangka")]
    public Button Button1;
    public Button Button2;

    [Header("Tombol Claim")]
    public Button ClaimJPKnya;
    public Button ClaimJPGnya;

    private void Start()
    {
        WaktuTextJangkaPendek.text = WaktuJangkaPendek.ToString();
        WaktuTextJangkaPanjang.text = WaktuJangkaPanjang.ToString();
        ClaimJPKnya.interactable = false;
        ClaimJPGnya.interactable = false;
    }

    private void Update()
    {
        int.TryParse(JumlahJangkaPendek.text, out InvestJangkaPendek);
        int.TryParse(JumlahJangkaPanjang.text, out InvestJangkaPanjang);
        Script = FindAnyObjectByType<UpiahManager>();
        Rupiah.text = Script.totalUpiah.ToString();
    }

    public void InvestasiJPK()
    {
        if (string.IsNullOrEmpty(JumlahJangkaPendek.text))
        {
            NotifMasukkanUpiah.ShowPopup();
            return;
        }

        if (Script.totalUpiah < InvestJangkaPendek)
        {
            NotifTdkCukup.ShowPopup();
            Button1.interactable = true;
        }
        else if (InvestJangkaPendek >= 15000)
        {
            Script.UpiahKurang(InvestJangkaPendek);
            totalPendapatanJangkaPendek = 0;
            isInvestingJPK = true;
            StartCoroutine(MulaiInvestasiJPK());
            Button1.interactable = false;
            ClaimJPKnya.interactable = false;
            BerhasilDibeli.ShowPopup();
        }
        else
        {
            NotifTdkCukup.ShowPopup();
        }
    }

    private IEnumerator MulaiInvestasiJPK()
    {
        for (int i = WaktuJangkaPendek; i > 0; i--)
        {
            if (!isInvestingJPK) yield break;

            if (i % 5 == 0)
            {
                totalPendapatanJangkaPendek += InvestJangkaPendek * PersenKeuntunganJPK;
            }

            if (i <= WaktuJangkaPendek - 5)
            {
                ClaimJPKnya.interactable = true;
            }
            else
            {
                ClaimJPKnya.interactable = false;
            }

            WaktuTextJangkaPendek.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        isInvestingJPK = false;
        WaktuTextJangkaPendek.text = WaktuJangkaPendek.ToString();
    }

    public void ClaimJPK()
    {
        Script.AddUpiah(Mathf.FloorToInt(totalPendapatanJangkaPendek));
        float keuntungan = Mathf.FloorToInt(totalPendapatanJangkaPendek) - InvestJangkaPendek;
        AngkaKeuntungan.text = keuntungan.ToString();
        totalPendapatanJangkaPendek = 0;
        isInvestingJPK = false;
        WaktuTextJangkaPendek.text = WaktuJangkaPendek.ToString();
        ClaimJPKnya.interactable = false;
        Button1.interactable = true;
        NotifKeuntungan.ShowUIWithDelay();
    }

    public void InvestasiJPG()
    {
        if (string.IsNullOrEmpty(JumlahJangkaPanjang.text))
        {
            NotifMasukkanUpiah.ShowPopup();
            return;
        }

        if (Script.totalUpiah < InvestJangkaPanjang)
        {
            NotifTdkCukup.ShowPopup();
            Button2.interactable = true;
        }
        else if (InvestJangkaPanjang >= 15000)
        {
            Script.UpiahKurang(InvestJangkaPanjang);
            totalPendapatanJangkaPanjang = 0;
            isInvestingJPG = true;
            StartCoroutine(MulaiInvestasiJPG());
            Button2.interactable = false;
            ClaimJPGnya.interactable = false;
            BerhasilDibeli.ShowPopup();
        }
        else
        {
            NotifTdkCukup.ShowPopup();
        }
    }

    private IEnumerator MulaiInvestasiJPG()
    {
        for (int i = WaktuJangkaPanjang; i > 0; i--)
        {
            if (!isInvestingJPG) yield break;

            if (i % 10 == 0)
            {
                totalPendapatanJangkaPanjang += InvestJangkaPanjang * PersenKeuntunganJPG;
            }

            if (i <= WaktuJangkaPanjang - 5)
            {
                ClaimJPGnya.interactable = true;
            }
            else
            {
                ClaimJPGnya.interactable = false;
            }

            WaktuTextJangkaPanjang.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        isInvestingJPG = false;
        WaktuTextJangkaPanjang.text = WaktuJangkaPanjang.ToString();
    }

    public void ClaimJPG()
    {
        Script.AddUpiah(Mathf.FloorToInt(totalPendapatanJangkaPanjang));
        float keuntungan = Mathf.FloorToInt(totalPendapatanJangkaPanjang) - InvestJangkaPanjang;
        AngkaKeuntungan.text = keuntungan.ToString();
        totalPendapatanJangkaPanjang = 0;
        isInvestingJPG = false;
        WaktuTextJangkaPanjang.text = WaktuJangkaPanjang.ToString();
        ClaimJPGnya.interactable = false;
        Button2.interactable = true;
        NotifKeuntungan.ShowUIWithDelay();
    }
}
