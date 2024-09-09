using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryCanvas;
    public MonoBehaviour playerMovementScript;
    public TextMeshProUGUI seedCountText;
    public TextMeshProUGUI seedRareCountText;  // UI untuk benih rare
    public TextMeshProUGUI monsterCountText;
    public TextMeshProUGUI bibitCountText;
    public TextMeshProUGUI bibitRareCountText;  // UI untuk bibit rare
    public TextMeshProUGUI pupukCountText;
    public TextMeshProUGUI koinFantasyText;
    public TextMeshProUGUI coinText;
    public Exchange exchangeScript;

    private bool isInventoryOpen = false;
    private int seedCount = 0;
    private int seedrare = 0; // Jumlah benih rare
    private int monsterCount = 0;
    private int bibitCount = 0;
    private int bibitrare = 0; // Jumlah bibit rare
    private int pupukCount = 0;
    private int KoinFantasy = 0;
    private int Coin = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isInventoryOpen && (exchangeScript == null || !exchangeScript.exchangeCanvas.gameObject.activeSelf))
        {
            OpenInventory();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isInventoryOpen)
        {
            CloseInventory();
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
        UpdateAllUI();
    }

    public void CloseInventory()
    {
        isInventoryOpen = false;
        inventoryCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerMovementScript.enabled = true;
    }

    // Update semua elemen UI
    private void UpdateAllUI()
    {
        UpdateSeedCountUI();
        UpdateSeedRareCountUI();
        UpdateMonsterCountUI();
        UpdateBibitCountUI();
        UpdateBibitRareCountUI();
        UpdatePupukCountUI();
        UpdateKoinFantasyUI();
        UpdateCoinUI();
    }


    // Benih biasa
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

    private void UpdateSeedCountUI()
    {
        if (seedCountText != null)
        {
            seedCountText.text = "Benih: " + seedCount;
        }
    }

    // Benih rare
    public void AddSeedRare(int amount)
    {
        seedrare += amount;
        UpdateSeedRareCountUI();
    }

    public bool HasSeedRare()
    {
        return seedrare > 0;
    }

    public void UseSeedRare()
    {
        if (seedrare > 0)
        {
            seedrare--;
            UpdateSeedRareCountUI();
        }
    }


    public int GetSeedRareCount()
    {
        return seedrare;
    }

    private void UpdateSeedRareCountUI()
    {
        if (seedRareCountText != null)
        {
            seedRareCountText.text = "Benih Rare: " + seedrare;
        }
    }

    // Bibit biasa
    public void AddBibit(int amount)
    {
        bibitCount += amount;
        UpdateBibitCountUI();
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

    private void UpdateBibitCountUI()
    {
        if (bibitCountText != null)
        {
            bibitCountText.text = "Bibit: " + bibitCount;
        }
    }

    // Bibit rare
    public void AddBibitRare(int amount)
    {
        bibitrare += amount;
        UpdateBibitRareCountUI();
    }

    public void UseBibitRare()
    {
        if (bibitrare > 0)
        {
            bibitrare--;
            UpdateBibitRareCountUI();
        }
    }

    public int GetBibitRareCount()
    {
        return bibitrare;
    }

    private void UpdateBibitRareCountUI()
    {
        if (bibitRareCountText != null)
        {
            bibitRareCountText.text = "Bibit Rare: " + bibitrare;
        }
    }

    // Monster
    public void AddMonster(int amount)
    {
        monsterCount += amount;
        UpdateMonsterCountUI();
    }

    public bool HasMonsters()
    {
        return monsterCount > 0;
    }

    public void UseMonster()
    {
        if (monsterCount > 0)
        {
            monsterCount--;
            UpdateMonsterCountUI();
        }
    }

    public int GetMonsterCount()
    {
        return monsterCount;
    }

    private void UpdateMonsterCountUI()
    {
        if (monsterCountText != null)
        {
            monsterCountText.text = "Monster: " + monsterCount;
        }
    }

   // Pupuk
    public void AddPupuk(int amount)
    {
        pupukCount += amount;
        UpdatePupukCountUI();
    }

    public bool HasPupuk()
    {
        return pupukCount > 0;
    }

    public void UsePupuk()
    {
        if (pupukCount > 0)
        {
            pupukCount--;
            UpdatePupukCountUI();
        }
    }

    public int GetPupukCount()
    {
        return pupukCount;
    }

    private void UpdatePupukCountUI()
    {
        if (pupukCountText != null)
        {
            pupukCountText.text = "Pupuk: " + pupukCount;
        }
    }


    // Koin Fantasy
    public void AddKoinFantasy(int amount)
    {
        KoinFantasy += amount;
        UpdateKoinFantasyUI();
    }

    public void UseKoinFantasy(int amount)
    {
        if (KoinFantasy >= amount)
        {
            KoinFantasy -= amount;
            UpdateKoinFantasyUI();
        }
    }

    public int GetKoinFantasy()
    {
        return KoinFantasy;
    }

    private void UpdateKoinFantasyUI()
    {
        if (koinFantasyText != null)
        {
            koinFantasyText.text = "Koin Fantasy: " + KoinFantasy;
        }
    }

    // Coin
    public void AddCoin(int amount)
    {
        Coin += amount;
        UpdateCoinUI();
    }

    public void UseCoin(int amount)
    {
        if (Coin >= amount)
        {
            Coin -= amount;
            UpdateCoinUI();
        }
    }

    public int GetCoin()
    {
        return Coin;
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coin: " + Coin;
        }
    }
}
