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

    [Header("Waktu Berjalan")]
    public int waktunyaJPK;
    public int waktunyaJPG;

    [Header("Uang Di Investkan")]
    public int DuitInvJPK;
    public int DuitInvJPG;

    [Header("Waktu Jangka")]
    public int WaktuJangkaPendek;
    public int WaktuJangkaPanjang;

    [Header("Tombol Jangka")]
    public Button Button1;
    public Button Button2;

    [Header("Tombol Claim")]
    public Button ClaimJPKnya;
    public Button ClaimJPGnya;

    [Header("Audio")]
    public AudioSource Beli;
    public AudioSource Claim;
    public AudioSource TidakCukup;

    private void Start()
    {
        WaktuTextJangkaPendek.text = WaktuJangkaPendek.ToString();
        WaktuTextJangkaPanjang.text = WaktuJangkaPanjang.ToString();
        ClaimJPKnya.interactable = false;
        ClaimJPGnya.interactable = false;
        LoadInvestmentData();
        if (isInvestingJPK)
        {
            Button1.interactable = false;
        }

        if (isInvestingJPG)
        {
            Button2.interactable = false;
        }
    }

    private void Update()
    {
        int.TryParse(JumlahJangkaPendek.text, out InvestJangkaPendek);
        int.TryParse(JumlahJangkaPanjang.text, out InvestJangkaPanjang);
        Script = FindAnyObjectByType<UpiahManager>();
        Rupiah.text = Script.totalUpiah.ToString();
    }
    private void OnApplicationQuit()
    {
        SaveInvestmentData();
    }

    private void SaveInvestmentData()
    {
        // Simpan waktu yang tersisa dan total pendapatan untuk jangka pendek
        if (isInvestingJPK)
        {
            PlayerPrefs.SetInt("WaktuJPK_Tersisa", waktunyaJPK);
            PlayerPrefs.SetFloat("TotalPendapatanJPK", totalPendapatanJangkaPendek);
            PlayerPrefs.SetInt("IsInvestingJPK", isInvestingJPK ? 1 : 0);
            PlayerPrefs.SetInt("InvestJangkaPendek", DuitInvJPK);
        }

        if (!isInvestingJPK)
        {
            PlayerPrefs.SetInt("IsInvestingJPK", isInvestingJPK ? 1 : 0);
            PlayerPrefs.DeleteKey("InvestJPK_Started");
            PlayerPrefs.DeleteKey("InvestJPK_TimeLeft");
            PlayerPrefs.DeleteKey("InvestJPK_Total");
        }

        // Simpan waktu yang tersisa dan total pendapatan untuk jangka panjang
        if (isInvestingJPG)
        {
            PlayerPrefs.SetInt("WaktuJPG_Tersisa", waktunyaJPG);
            PlayerPrefs.SetFloat("TotalPendapatanJPG", totalPendapatanJangkaPanjang);
            PlayerPrefs.SetInt("IsInvestingJPG", isInvestingJPG ? 1 : 0);
            PlayerPrefs.SetInt("InvestJangkaPanjang", DuitInvJPG);
        }

        if (!isInvestingJPK)
        {
            PlayerPrefs.SetInt("IsInvestingJPG", isInvestingJPK ? 1 : 0);
            PlayerPrefs.DeleteKey("InvestJPG_Started");
            PlayerPrefs.DeleteKey("InvestJPG_TimeLeft");
            PlayerPrefs.DeleteKey("InvestJPG_Total");
        }

        PlayerPrefs.Save();  // Pastikan untuk menyimpan perubahan
    }

    private void LoadInvestmentData()
    {
        // Muat data untuk jangka pendek
        if (PlayerPrefs.HasKey("IsInvestingJPK") && PlayerPrefs.GetInt("IsInvestingJPK") == 1)
        {
            waktunyaJPK = PlayerPrefs.GetInt("WaktuJPK_Tersisa");
            totalPendapatanJangkaPendek = PlayerPrefs.GetFloat("TotalPendapatanJPK");
            DuitInvJPK = PlayerPrefs.GetInt("InvestJangkaPendek");
            isInvestingJPK = true;
            StartCoroutine(MulaiInvestasiJPK());
        }

        // Muat data untuk jangka panjang
        if (PlayerPrefs.HasKey("IsInvestingJPG") && PlayerPrefs.GetInt("IsInvestingJPG") == 1)
        {
            waktunyaJPG = PlayerPrefs.GetInt("WaktuJPG_Tersisa");
            totalPendapatanJangkaPanjang = PlayerPrefs.GetFloat("TotalPendapatanJPG");
            DuitInvJPG = PlayerPrefs.GetInt("InvestJangkaPanjang");
            isInvestingJPG = true;
            StartCoroutine(MulaiInvestasiJPG());
        }
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
            waktunyaJPK = WaktuJangkaPendek;
            DuitInvJPK = InvestJangkaPendek;
            StartCoroutine(MulaiInvestasiJPK());
            Button1.interactable = false;
            ClaimJPKnya.interactable = false;
            BerhasilDibeli.ShowPopup();
            SaveInvestmentData();
            Beli.Play();
        }
        else
        {
            NotifTdkCukup.ShowPopup();
            TidakCukup.Play();
        }
    }

    private IEnumerator MulaiInvestasiJPK()
    {
        for (int i = waktunyaJPK; i > 0; i--)
        {
            if (!isInvestingJPK) yield break;

            if (i % 5 == 0)  // Setiap 5 detik
            {
                totalPendapatanJangkaPendek += DuitInvJPK * PersenKeuntunganJPK;
            }

            if (i <= waktunyaJPK - 5)
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
        WaktuTextJangkaPendek.text = waktunyaJPK.ToString();
    }

    public void ClaimJPK()
    {
        Script.AddUpiah(Mathf.FloorToInt(totalPendapatanJangkaPendek));
        float keuntungan = Mathf.FloorToInt(totalPendapatanJangkaPendek) - DuitInvJPK;
        AngkaKeuntungan.text = keuntungan.ToString();
        totalPendapatanJangkaPendek = 0;
        isInvestingJPK = false;
        WaktuTextJangkaPendek.text = WaktuJangkaPendek.ToString();
        ClaimJPKnya.interactable = false;
        Button1.interactable = true;
        NotifKeuntungan.ShowUIWithDelay();
        Claim.Play();
        PlayerPrefs.DeleteKey("InvestJPK_Started");
        PlayerPrefs.DeleteKey("InvestJPK_TimeLeft");
        PlayerPrefs.DeleteKey("InvestJPK_Total");
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
            DuitInvJPG = InvestJangkaPanjang;
            waktunyaJPG = WaktuJangkaPanjang;
            StartCoroutine(MulaiInvestasiJPG());
            Button2.interactable = false;
            ClaimJPGnya.interactable = false;
            BerhasilDibeli.ShowPopup();
            SaveInvestmentData();
            Beli.Play();
        }
        else
        {
            NotifTdkCukup.ShowPopup();
            TidakCukup.Play();
        }
    }

    private IEnumerator MulaiInvestasiJPG()
    {
        for (int i = waktunyaJPG; i > 0; i--)
        {
            if (!isInvestingJPG) yield break;

            if (i % 10 == 0)  // Setiap 10 detik
            {
                totalPendapatanJangkaPanjang += DuitInvJPG * PersenKeuntunganJPG;
            }

            if (i <= waktunyaJPG - 5)
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
        WaktuTextJangkaPanjang.text = waktunyaJPG.ToString();
        SaveInvestmentData();  // Save data setiap akhir investasi
    }

    public void ClaimJPG()
    {
        Claim.Play();
        Script.AddUpiah(Mathf.FloorToInt(totalPendapatanJangkaPanjang));
        float keuntungan = Mathf.FloorToInt(totalPendapatanJangkaPanjang) - DuitInvJPG;
        AngkaKeuntungan.text = keuntungan.ToString();
        totalPendapatanJangkaPanjang = 0;
        isInvestingJPG = false;
        WaktuTextJangkaPanjang.text = WaktuJangkaPanjang.ToString();
        ClaimJPGnya.interactable = false;
        Button2.interactable = true;
        NotifKeuntungan.ShowUIWithDelay();
        PlayerPrefs.DeleteKey("InvestJPG_Started");
        PlayerPrefs.DeleteKey("InvestJPG_TimeLeft");
        PlayerPrefs.DeleteKey("InvestJPG_Total");
    }
}
