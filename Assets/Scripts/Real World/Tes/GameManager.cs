using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text coinText;

    private int totalCoins = 0;

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
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateCoinText();
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        UpdateCoinText();
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
