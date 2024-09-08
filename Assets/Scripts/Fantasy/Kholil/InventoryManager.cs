using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryCanvas;
    public MonoBehaviour playerMovementScript;
    public TextMeshProUGUI seedCountText;
    public TextMeshProUGUI monsterCountText;
    public TextMeshProUGUI bibitCountText;  // Reference for bibit count
    public TextMeshProUGUI pupukCountText;  // Reference for pupuk count
    public TextMeshProUGUI koinFantasyText;  // Reference for Koin Fantasy
    public TextMeshProUGUI coinText;  // Reference for Coin count
    public Exchange exchangeScript;  // Reference to Exchange script

    private bool isInventoryOpen = false;
    private int seedCount = 0;
    private int monsterCount = 0;
    private int bibitCount = 0;
    private int pupukCount = 0;  // Separate count for pupuk
    private int KoinFantasy = 0;  // Koin Fantasy count
    private int Coin = 0;  // Coin count
    private int AttackSkill2 = 0;
    private int AttackSkill3 = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isInventoryOpen && (exchangeScript == null || !exchangeScript.exchangeCanvas.gameObject.activeSelf))
        {
            OpenInventory();
        }
    }

    public void OpenInventory()
    {
        isInventoryOpen = true;
        inventoryCanvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
        playerMovementScript.enabled = false;
        UpdateSeedCountUI();
        UpdateMonsterCountUI();
        UpdateBibitCountUI();  // Update bibit UI
        UpdatePupukCountUI();  // Update pupuk UI
        UpdateKoinFantasyUI();  // Update Koin Fantasy UI
        UpdateCoinUI();  // Update Coin UI
    }

    public void CloseInventory()
    {
        isInventoryOpen = false;
        inventoryCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerMovementScript.enabled = true;
    }

    public void AddSeed(int amount)
    {
        seedCount += amount;
        UpdateSeedCountUI();
    }

    public bool HasSeeds()
    {
        return seedCount > 0;
    }

    public void UseSeed()
    {
        if (seedCount > 0)
        {
            seedCount--;
            UpdateSeedCountUI();
        }
    }

    public int GetSeedCount()
    {
        return seedCount;
    }

    public void AddMonster(int amount)
    {
        monsterCount += amount;
        UpdateMonsterCountUI();
    }

    public int GetMonsterCount()
    {
        return monsterCount;
    }

    public void AddBibit(int amount)
    {
        bibitCount += amount;
        UpdateBibitCountUI();  // Update UI when bibit count changes
    }

    public void UseBibit()
    {
        if (bibitCount > 0)
        {
            bibitCount--;
            UpdateBibitCountUI();
        }
    }

    public int GetBibitCount()
    {
        return bibitCount;
    }

    public void AddPupuk(int amount)
    {
        pupukCount += amount;
        UpdatePupukCountUI();  // Update UI when pupuk count changes
    }

    public int GetPupukCount()
    {
        return pupukCount;
    }

    public void AddKoinFantasy(int amount)
    {
        KoinFantasy += amount;
        UpdateKoinFantasyUI();
    }

    public int GetKoinFantasy()
    {
        return KoinFantasy;
    }

    public void AddCoin(int amount)
    {
        Coin += amount;
        UpdateCoinUI();

        // Laporkan jumlah koin ke GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddCoins(amount);
        }
    }

    public int GetCoin()
    {
        return Coin;
    }

    private void UpdateSeedCountUI()
    {
        if (seedCountText != null)
        {
            seedCountText.text = "Benih: " + seedCount;
        }
    }

    private void UpdateMonsterCountUI()
    {
        if (monsterCountText != null)
        {
            monsterCountText.text = "Monster: " + monsterCount;
        }
    }

    private void UpdateBibitCountUI()
    {
        if (bibitCountText != null)
        {
            bibitCountText.text = "Bibit: " + bibitCount;
        }
    }

    private void UpdatePupukCountUI()
    {
        if (pupukCountText != null)
        {
            pupukCountText.text = "Pupuk: " + pupukCount;
        }
    }

    private void UpdateKoinFantasyUI()
    {
        if (koinFantasyText != null)
        {
            koinFantasyText.text = "Koin Fantasy: " + KoinFantasy;
        }
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Rupiah: " + Coin;
        }
    }
}
