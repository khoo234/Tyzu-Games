using System.Collections;
using TMPro;
using UnityEngine;

public class UpiahManager : MonoBehaviour
{
    public static UpiahManager Instance;
    public TMP_Text UpiahText;
    public TMP_Text UpiahTampilkan;

    public int totalUpiah = 0;
    private const string upiahKey = "TotalUpiah";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        totalUpiah = PlayerPrefs.GetInt(upiahKey, 0);
        UpdateUpiahText();
    }

    private void Update()
    {
        UpdateUpiahText();
    }

    public void AddUpiah(int amount)
    {
        totalUpiah += amount;
        UpdateUpiahText();
        PlayerPrefs.SetInt(upiahKey, totalUpiah);
    }

    public void UpiahKurang(int Jumlahnya)
    {
        totalUpiah -= Jumlahnya;
        UpdateUpiahText();
        PlayerPrefs.SetInt(upiahKey, totalUpiah);
    }

    public int GetTotalUpiah()
    {
        return totalUpiah;
    }

    private void UpdateUpiahText()
    {
        UpiahText.text = "Upiah: " + totalUpiah;
        UpiahTampilkan.text = totalUpiah.ToString();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(upiahKey, totalUpiah);
    }
}
