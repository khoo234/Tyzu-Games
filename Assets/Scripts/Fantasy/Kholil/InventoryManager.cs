using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Script")]
    public MonoBehaviour playerMovementScript;
    public Exchange exchangeScript;

    [Header("Jumlah Seed Basic")]
    public TextMeshProUGUI seedCountText;
    public TextMeshProUGUI seedRareCountText;  // UI untuk benih rare
    public TextMeshProUGUI monsterCountText;
    public TextMeshProUGUI bibitCountText;
    public TextMeshProUGUI bibitRareCountText;  // UI untuk bibit rare
    public TextMeshProUGUI pupukCountText;
    public TextMeshProUGUI koinFantasyText;

    [Header ("Jumlah")]
    public int seedCount = 0;
    public int seedrare = 0;
    public int monsterCount = 0;
    public int bibitCount = 0;
    public int bibitrare = 0;
    public int pupukCount = 0;
    public int KoinFantasy = 0;

    [Header("Fungsi Backpack")]
    public UnityEvent BukaBackPack;
    public UnityEvent TutupBackPack;

    [Header("Button")]
    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;
    public Button Button5;
    public Button Button6;
    public Button Button7;
    public Button Button8;
    public Button Button9;

    private bool isInventoryOpen = false;

    void Start()
    {
        Tombol1();
        UpdateAllUI();
        LoadInventoryData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isInventoryOpen)
        {
            OpenInventory();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isInventoryOpen)
        {
            CloseInventory();
        }
    }

    public void SaveInventoryData()
    {
        PlayerPrefs.SetInt("SeedCount", seedCount);
        PlayerPrefs.SetInt("SeedRareCount", seedrare);
        PlayerPrefs.SetInt("MonsterCount", monsterCount);
        PlayerPrefs.SetInt("BibitCount", bibitCount);
        PlayerPrefs.SetInt("BibitRareCount", bibitrare);
        PlayerPrefs.SetInt("PupukCount", pupukCount);
        PlayerPrefs.SetInt("KoinFantasy", KoinFantasy);
        PlayerPrefs.Save();
    }

    public void LoadInventoryData()
    {
        seedCount = PlayerPrefs.GetInt("SeedCount", 0);
        seedrare = PlayerPrefs.GetInt("SeedRareCount", 0);
        monsterCount = PlayerPrefs.GetInt("MonsterCount", 0);
        bibitCount = PlayerPrefs.GetInt("BibitCount", 0);
        bibitrare = PlayerPrefs.GetInt("BibitRareCount", 0);
        pupukCount = PlayerPrefs.GetInt("PupukCount", 0);
        KoinFantasy = PlayerPrefs.GetInt("KoinFantasy", 0);

        UpdateAllUI();
    }

    public void Kosongan()
    {
        seedCount = 0;
        seedrare = 0;
        monsterCount = 0;
        bibitCount = 0;
        bibitrare = 0;
        pupukCount = 0;
        KoinFantasy = 0;
    }

    public void OpenInventory()
    {
        isInventoryOpen = true;
        BukaBackPack?.Invoke();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
        playerMovementScript.enabled = false;
        UpdateAllUI();
    }

    public void CloseInventory()
    {
        SaveInventoryData();
        isInventoryOpen = false;
        TutupBackPack?.Invoke();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerMovementScript.enabled = true;
    }

    // Atau, simpan data saat game di-quit
    void OnApplicationQuit()
    {
        SaveInventoryData();
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

    public void UseBibitRare()
    {
        if (bibitrare > 0)
        {
            bibitrare--;
            UpdateBibitRareCountUI();
        }
    }

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

    public void AddBibitRare(int amount)
    {
        bibitrare += amount;
        UpdateBibitRareCountUI();
    }

    public int GetBibitRareCount()
    {
        return bibitrare;
    }

    public void AddPupuk(int amount)
    {
        pupukCount += amount;
        UpdatePupukCountUI();
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

    private void UpdateAllUI()
    {
        UpdateSeedCountUI();
        UpdateSeedRareCountUI();
        UpdateMonsterCountUI();
        UpdateBibitCountUI();
        UpdateBibitRareCountUI();
        UpdatePupukCountUI();
        UpdateKoinFantasyUI();
    }

    private void UpdateSeedCountUI()
    {
        if (seedCountText != null)
        {
            seedCountText.text = seedCount.ToString();
        }
    }

    private void UpdateSeedRareCountUI()
    {
        if (seedRareCountText != null)
        {
            seedRareCountText.text =  seedrare.ToString();
        }
    }

    private void UpdateMonsterCountUI()
    {
        if (monsterCountText != null)
        {
            monsterCountText.text = monsterCount.ToString();
        }
    }

    private void UpdateBibitCountUI()
    {
        if (bibitCountText != null)
        {
            bibitCountText.text = bibitCount.ToString();
        }
    }

    private void UpdateBibitRareCountUI()
    {
        if (bibitRareCountText != null)
        {
            bibitRareCountText.text = bibitrare.ToString();
        }
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

    private void UpdatePupukCountUI()
    {
        if (pupukCountText != null)
        {
            pupukCountText.text = pupukCount.ToString();
        }
    }

    private void UpdateKoinFantasyUI()
    {
        if (koinFantasyText != null)
        {
            koinFantasyText.text = KoinFantasy.ToString();
        }
    }

    public void Tombol1()
    {
        SetButtonInteractable(Button1);
    }

    public void Tombol2()
    {
        SetButtonInteractable(Button2);
    }

    public void Tombol3()
    {
        SetButtonInteractable(Button3);
    }

    public void Tombol4()
    {
        SetButtonInteractable(Button4);
    }

    public void Tombol5()
    {
        SetButtonInteractable(Button5);
    }

    public void Tombol6()
    {
        SetButtonInteractable(Button6);
    }

    public void Tombol7()
    {
        SetButtonInteractable(Button7);
    }

    public void Tombol8()
    {
        SetButtonInteractable(Button8);
    }

    public void Tombol9()
    {
        SetButtonInteractable(Button9);
    }

    private void SetButtonInteractable(Button activeButton)
    {
        Button1.interactable = true;
        Button2.interactable = true;
        Button3.interactable = true;
        Button4.interactable = true;
        Button5.interactable = true;
        Button6.interactable = true;
        Button7.interactable = true;
        Button8.interactable = true;
        Button9.interactable = true;

        activeButton.interactable = false;

        EventSystem.current.SetSelectedGameObject(activeButton.gameObject);
    }
}
