using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Exchange : MonoBehaviour
{
    public Canvas exchangeCanvas;
    private bool isInTrigger = false;
    public MonoBehaviour playerMovementScript;

    public TextMeshProUGUI exchangeMonsterText;
    public TextMeshProUGUI exchangeBibitText;
    public TextMeshProUGUI exchangeKoinFantasyText; // TMP untuk Koin Fantasy
    public InventoryManager inventoryManager;

    public Button exchangeButton;
    public Button koinFantasyButton;
    public Button coinButton;  // Tombol untuk menukar Koin Fantasy ke Coin

    void Start()
    {
        if (exchangeCanvas != null)
        {
            exchangeCanvas.gameObject.SetActive(false);
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (exchangeButton != null)
        {
            exchangeButton.onClick.AddListener(OnExchangeButtonClick);
        }

        if (koinFantasyButton != null)
        {
            koinFantasyButton.onClick.AddListener(OnKoinFantasyButtonClick);
        }

        if (coinButton != null)
        {
            coinButton.onClick.AddListener(OnCoinButtonClick);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInTrigger)
        {
            ToggleCanvas();
        }
    }

    private void ToggleCanvas()
    {
        if (exchangeCanvas != null)
        {
            bool isCanvasActive = !exchangeCanvas.gameObject.activeSelf;
            exchangeCanvas.gameObject.SetActive(isCanvasActive);

            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = !isCanvasActive;
            }

            Cursor.visible = isCanvasActive;
            Cursor.lockState = isCanvasActive ? CursorLockMode.None : CursorLockMode.Locked;

            if (isCanvasActive && inventoryManager != null)
            {
                UpdateExchangeCanvas();
            }
        }
    }

    private void UpdateExchangeCanvas()
    {
        if (exchangeMonsterText != null && inventoryManager != null)
        {
            exchangeMonsterText.text = "Monster: " + inventoryManager.GetMonsterCount();
        }

        if (exchangeBibitText != null && inventoryManager != null)
        {
            exchangeBibitText.text = "Bibit: " + inventoryManager.GetBibitCount();
        }

        if (exchangeKoinFantasyText != null && inventoryManager != null)
        {
            exchangeKoinFantasyText.text = "Koin Fantasy: " + inventoryManager.GetKoinFantasy();
        }
    }

    private void OnExchangeButtonClick()
    {
        if (inventoryManager.GetMonsterCount() >= 2)
        {
            inventoryManager.AddMonster(-2);
            inventoryManager.AddPupuk(1);
            UpdateExchangeCanvas();
        }
    }

    private void OnKoinFantasyButtonClick()
    {
        if (inventoryManager.GetBibitCount() >= 1)
        {
            inventoryManager.UseBibit();
            inventoryManager.AddKoinFantasy(500);
            UpdateExchangeCanvas();
        }
    }

    private void OnCoinButtonClick()  // Fungsi untuk menukar Koin Fantasy menjadi Coin
    {
        if (inventoryManager.GetKoinFantasy() >= 1000)
        {
            inventoryManager.AddKoinFantasy(-1000);
            inventoryManager.AddCoin(1000000);  // Tambahkan Coin ke inventory

            // Laporkan jumlah koin ke GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddCoins(1000000); // Tambahkan ke total koin di GameManager
            }

            UpdateExchangeCanvas();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = false;
        }
    }
}
