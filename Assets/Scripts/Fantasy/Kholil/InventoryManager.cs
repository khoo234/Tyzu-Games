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

    [Header("Additional Counts")]
    public TextMeshProUGUI monsterCountText;
    public TextMeshProUGUI bibitCountText;
    public TextMeshProUGUI pupukCountText;
    public TextMeshProUGUI koinFantasyText;
    public TextMeshProUGUI coinText;

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
    private int seedCount = 0;
    private int monsterCount = 0;
    private int bibitCount = 0;
    private int pupukCount = 0;
    private int KoinFantasy = 0;
    private int Coin = 0;

    void Start()
    {
        Tombol1();
        UpdateSeedCountUI();
    }

    void Update()
    {
        if (!isInventoryOpen)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && (exchangeScript == null || !exchangeScript.exchangeCanvas.gameObject.activeSelf))
            {
                OpenInventory();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                CloseInventory();
            }
        }
    }

    public void OpenInventory()
    {
        isInventoryOpen = true;
        BukaBackPack?.Invoke();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
        playerMovementScript.enabled = false;
        UpdateUI();
    }

    public void CloseInventory()
    {
        isInventoryOpen = false;
        TutupBackPack?.Invoke();
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

    public void AddCoin(int amount)
    {
        Coin += amount;
        UpdateCoinUI();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddCoins(amount);
        }
    }

    public int GetCoin()
    {
        return Coin;
    }

    private void UpdateUI()
    {
        UpdateSeedCountUI();
        UpdateMonsterCountUI();
        UpdateBibitCountUI();
        UpdatePupukCountUI();
        UpdateKoinFantasyUI();
        UpdateCoinUI();
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
