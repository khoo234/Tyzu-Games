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
    public TextMeshProUGUI exchangeBibitRareText;
    public TextMeshProUGUI exchangeKoinFantasyText; // TMP untuk Koin Fantasy
    public InventoryManager inventoryManager;

    public Button exchangeButton;
    public Button koinFantasyButton;
    public Button koinFantasyRareButton;
    public Button coinButton;  // Tombol untuk menukar Koin Fantasy ke Coin

    public Attack playerAttack; // Reference ke skrip Attack untuk mengaktifkan skill
    public Button attackSkill2Button; // Tombol untuk menukar Attack Skill LV2
    public Button attackSkill3Button; // Tombol untuk menukar Attack Skill LV3

    public Skill2 playerAttack2; // Reference ke skrip Skill2 untuk mengaktifkan skill2
    public Button attack2Skill2Button; // Tombol untuk menukar Skill2 Skill LV2
    public Button attack2Skill3Button; // Tombol untuk menukar Skill2 Skill LV3

    public Ulti playerAttack3; // Reference ke skrip Ulti untuk mengaktifkan skill3
    public Button attack3Skill2Button; // Tombol untuk menukar Ulti Skill LV2
    public Button attack3Skill3Button; // Tombol untuk menukar Ulti Skill LV3

    void Start()
    {
        playerAttack3 = FindAnyObjectByType<Ulti>();
        playerAttack2 = FindAnyObjectByType<Skill2>();
        playerAttack = FindAnyObjectByType<Attack>();

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
        if (koinFantasyRareButton != null)
        {
            koinFantasyRareButton.onClick.AddListener(OnKoinFantasyRareButtonClick);
        }

        if (coinButton != null)
        {
            coinButton.onClick.AddListener(OnCoinButtonClick);
        }

        if (attackSkill2Button != null)
        {
            attackSkill2Button.onClick.AddListener(OnAttackSkill2ButtonClick);
        }

        if (attackSkill3Button != null)
        {
            attackSkill3Button.onClick.AddListener(OnAttackSkill3ButtonClick);
        }

        if (attack2Skill2Button != null)
        {
            attack2Skill2Button.onClick.AddListener(OnAttack2Skill2ButtonClick);
        }

        if (attack2Skill3Button != null)
        {
            attack2Skill3Button.onClick.AddListener(OnAttack2Skill3ButtonClick);
        }

        if (attack3Skill2Button != null)
        {
            attack3Skill2Button.onClick.AddListener(OnAttack3Skill2ButtonClick);
        }

        if (attack3Skill3Button != null)
        {
            attack3Skill3Button.onClick.AddListener(OnAttack3Skill3ButtonClick);
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
        if (exchangeBibitRareText != null && inventoryManager != null)
        {
            exchangeBibitRareText.text = "BibitRare: " + inventoryManager.GetBibitRareCount();
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

    private void OnKoinFantasyRareButtonClick()  // Fungsi untuk menukar BibitRare menjadi Koin Fantasy
    {
        if (inventoryManager.GetBibitRareCount() >= 1)
        {
            inventoryManager.UseBibitRare();  // Kurangi jumlah BibitRare
            inventoryManager.AddKoinFantasy(1500);  // Tambahkan 1500 Koin Fantasy
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

    private void OnAttackSkill2ButtonClick() // Fungsi untuk menukar monster dan Koin Fantasy menjadi Attack Skill LV2
    {
        if (inventoryManager.GetMonsterCount() >= 1 && inventoryManager.GetKoinFantasy() >= 500)
        {
            inventoryManager.AddMonster(-1);
            inventoryManager.AddKoinFantasy(-500);

            // Aktifkan level 2 di skrip Attack
            if (playerAttack != null)
            {
                playerAttack.lv2 = true;
                Debug.Log("Attack Skill Level 2 Unlocked!");
            }

            UpdateExchangeCanvas();
        }
    }

    private void OnAttackSkill3ButtonClick() // Fungsi untuk menukar monster dan Koin Fantasy menjadi Attack Skill LV3
    {
        if (inventoryManager.GetMonsterCount() >= 1 && inventoryManager.GetKoinFantasy() >= 1000)
        {
            inventoryManager.AddMonster(-1);
            inventoryManager.AddKoinFantasy(-1000);

            // Aktifkan level 3 di skrip Attack
            if (playerAttack != null)
            {
                playerAttack.lv3 = true;
                Debug.Log("Attack Skill Level 3 Unlocked!");
            }

            UpdateExchangeCanvas();
        }
    }

    private void OnAttack2Skill2ButtonClick()
    {
        if (inventoryManager.GetMonsterCount() >= 1 && inventoryManager.GetKoinFantasy() >= 500)
        {
            inventoryManager.AddMonster(-1);
            inventoryManager.AddKoinFantasy(-500);

            // Set skill level 2 in Skill2 script
            if (playerAttack2 != null)
            {
                playerAttack2.SetSkillLevel(2);
                Debug.Log("Attack Skill Level 2 Unlocked!");
            }

            UpdateExchangeCanvas();
        }
    }

    private void OnAttack2Skill3ButtonClick()
    {
        if (inventoryManager.GetMonsterCount() >= 1 && inventoryManager.GetKoinFantasy() >= 1000)
        {
            inventoryManager.AddMonster(-1);
            inventoryManager.AddKoinFantasy(-1000);

            // Set skill level 3 in Skill2 script
            if (playerAttack2 != null)
            {
                playerAttack2.SetSkillLevel(3);
                Debug.Log("Attack Skill Level 3 Unlocked!");
            }

            UpdateExchangeCanvas();
        }
    }

    private void OnAttack3Skill2ButtonClick()
    {
        if (inventoryManager.GetMonsterCount() >= 1 && inventoryManager.GetKoinFantasy() >= 500)
        {
            inventoryManager.AddMonster(-1);
            inventoryManager.AddKoinFantasy(-500);

            // Set skill level 2 in Ulti script
            if (playerAttack3 != null)
            {
                playerAttack3.SetSkillLevel(2);
                Debug.Log("Ultimate Skill Level 2 Unlocked!");
            }

            UpdateExchangeCanvas();
        }
    }

    private void OnAttack3Skill3ButtonClick()
    {
        if (inventoryManager.GetMonsterCount() >= 1 && inventoryManager.GetKoinFantasy() >= 1000)
        {
            inventoryManager.AddMonster(-1);
            inventoryManager.AddKoinFantasy(-1000);

            // Set skill level 3 in Ulti script
            if (playerAttack3 != null)
            {
                playerAttack3.SetSkillLevel(3);
                Debug.Log("Ultimate Skill Level 3 Unlocked!");
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
            if (exchangeCanvas != null)
            {
                exchangeCanvas.gameObject.SetActive(false);
                playerMovementScript.enabled = true;

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
