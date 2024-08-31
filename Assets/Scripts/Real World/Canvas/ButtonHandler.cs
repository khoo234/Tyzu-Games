using TMPro; // Menggunakan TextMeshPro
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonHandler : MonoBehaviour
{
    public Button investasiButton; // Referensi ke tombol Investasi
    public Button playButton; // Referensi ke tombol Play
    public GameObject panelInvestasi; // Referensi ke Panel Investasi

    public Button button2X; // Referensi ke tombol 2X
    public Button button4X; // Referensi ke tombol 4X
    public TMP_Text timerText; // Referensi ke TMP_Text untuk menampilkan timer
    public TMP_Text coinText; // Referensi ke TMP_Text untuk menampilkan jumlah koin

    private bool isInvesting = false; // Flag untuk mengecek apakah investasi sedang berjalan

    private void Start()
    {
        panelInvestasi.SetActive(false);

        // Menambahkan listener untuk tombol Investasi
        if (investasiButton != null)
        {
            investasiButton.onClick.AddListener(OnInvestasiButtonClick);
        }
        else
        {
            Debug.LogError("Investasi Button not assigned in the Inspector.");
        }

        // Menambahkan listener untuk tombol Play
        if (playButton != null)
        {
            playButton.onClick.AddListener(OnPlayButtonClick);
            playButton.gameObject.SetActive(true); // Pastikan tombol Play aktif
        }
        else
        {
            Debug.LogError("Play Button not assigned in the Inspector.");
        }

        // Menambahkan listener untuk tombol 2X
        if (button2X != null)
        {
            button2X.onClick.AddListener(() => StartCoroutine(StartInvestment(button2X, 2, 10f)));
        }
        else
        {
            Debug.LogError("Button 2X not assigned in the Inspector.");
        }

        // Menambahkan listener untuk tombol 4X
        if (button4X != null)
        {
            button4X.onClick.AddListener(() => StartCoroutine(StartInvestment(button4X, 4, 20f)));
        }
        else
        {
            Debug.LogError("Button 4X not assigned in the Inspector.");
        }

        // Update UI awal untuk koin
        UpdateCoinText();
    }

    private void OnInvestasiButtonClick()
    {
        if (playButton != null)
        {
            playButton.gameObject.SetActive(false); // Nonaktifkan tombol Play
        }

        if (panelInvestasi != null)
        {
            panelInvestasi.SetActive(true); // Aktifkan panel Investasi
        }

        if (investasiButton != null)
        {
            investasiButton.gameObject.SetActive(false); // Nonaktifkan tombol Investasi
        }
    }

    private void OnPlayButtonClick()
    {
        // Logika untuk tombol Play, jika diperlukan
        if (panelInvestasi != null)
        {
            panelInvestasi.SetActive(false); // Nonaktifkan panel Investasi
        }

        if (playButton != null)
        {
            playButton.gameObject.SetActive(true); // Aktifkan tombol Play
        }

        if (investasiButton != null)
        {
            investasiButton.gameObject.SetActive(true); // Aktifkan kembali tombol Investasi jika diperlukan
        }
    }

    private IEnumerator StartInvestment(Button clickedButton, int multiplier, float duration)
    {
        // Jika investasi sedang berjalan, abaikan klik lainnya
        if (isInvesting)
            yield break;

        // Tandai bahwa investasi sedang berjalan
        isInvesting = true;

        // Nonaktifkan tombol yang diklik
        clickedButton.interactable = false;

        float timeRemaining = duration;
        int initialCoins = GameManager.Instance.GetTotalCoins();
        int finalCoins = initialCoins * multiplier;

        // Menampilkan timer
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "Timer: " + Mathf.Ceil(timeRemaining).ToString() + "s";
            yield return null;
        }

        // Update koin setelah timer selesai
        GameManager.Instance.AddCoins(finalCoins - initialCoins);

        // Timer selesai, kosongkan text timer
        timerText.text = "";

        // Aktifkan kembali tombol setelah timer selesai
        clickedButton.interactable = true;

        // Tandai bahwa investasi telah selesai
        isInvesting = false;

        // Update koin di UI ButtonHandler
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        // Update coin text di UI ButtonHandler
        if (coinText != null)
        {
            coinText.text = "Coins: " + GameManager.Instance.GetTotalCoins();
        }
        else
        {
            Debug.LogError("coinText is not assigned in ButtonHandler.");
        }
    }
}
