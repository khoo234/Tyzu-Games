using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    public TMP_Text coinText; // Referensi ke TMP_Text component di Panel Investasi

    private int totalCoins = 0; // Jumlah koin yang dikumpulkan

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Jangan hancurkan GameManager saat berpindah scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Muat data koin dari PlayerPrefs
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateCoinText();
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        UpdateCoinText();
        // Simpan jumlah koin ke PlayerPrefs
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        Debug.Log("Coins Added: " + amount + ". Total Coins: " + totalCoins);
    }

    public int GetTotalCoins()
    {
        return totalCoins;
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Koin: " + totalCoins;
        }
        else
        {
            Debug.LogError("coinText is not assigned in GameManager.");
        }
    }

    private void OnApplicationQuit()
    {
        // Simpan jumlah koin saat aplikasi keluar
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
    }
}
