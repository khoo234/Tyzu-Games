using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;

public class Exchange : MonoBehaviour
{
    [Header ("Animasi")]
    public Animasi buka;
    public Notifikasi berhasilbeli;
    public Notifikasi berhasildiupgrade;
    public Notifikasi berhasiltukar;
    public Notifikasi tidakpunya;
    public Notifikasi tidakcukuptukar;
    public Notifikasi tidakbisaupgrade;

    private bool isInTrigger = false;
    private bool bukatoko = false;

    [Header ("Script")]
    public MonoBehaviour playerMovementScript;
    public Attack playerAttack;
    public Skill2 playerSkill;
    public Ulti playerUlti;
    public InventoryManager inventoryManager;
    public InformasiUpgrade InfoApgred;

    [Header("Attack Level")]
    public bool AttackLevel2;
    public bool AttackLevel3;

    [Header("Skill Level")]
    public bool SkillLevel2;
    public bool SkillLevel3;

    [Header("Ulti Level")]
    public bool UltiLevel2;
    public bool UltiLevel3;

    [Header("Text")]
    public TextMeshProUGUI TextBibitBiasa;
    public TextMeshProUGUI TextBibitRare;
    public TextMeshProUGUI TextKoin;
    public TextMeshProUGUI TextHama;

    [Header ("Buka")]
    public UnityEvent bukatokoo;

    [Header ("Tutup")]
    public UnityEvent tutuptokoo;


    void Start()
    {
        playerUlti = FindAnyObjectByType<Ulti>();
        playerSkill = FindAnyObjectByType<Skill2>();
        playerAttack = FindAnyObjectByType<Attack>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!bukatoko)
        {
            if (Input.GetKeyDown(KeyCode.E) && isInTrigger)
            {
                buka.ShowUI();
                BukaToko();
                bukatoko = true;
                bukatokoo?.Invoke();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E) && isInTrigger)
            {
                buka.HideUI();
                bukatoko = false;
                tutuptokoo?.Invoke();
            }
        }
    }

    public void BukaToko()
    {
        playerMovementScript.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        UpdateExchangeCanvas();
    }

    private void UpdateExchangeCanvas()
    {
        TextHama.text = inventoryManager.GetMonsterCount().ToString();
        TextBibitBiasa.text = inventoryManager.GetBibitCount().ToString();
        TextBibitRare.text = inventoryManager.GetBibitRareCount().ToString();
        TextKoin.text = inventoryManager.GetKoinFantasy().ToString();
    }

    public void TukarJadiPupuk()
    {
        if (inventoryManager.GetMonsterCount() >= 2)
        {
            inventoryManager.AddMonster(-2);
            inventoryManager.AddPupuk(1);
            UpdateExchangeCanvas();
        }
        else
        {
            tidakcukuptukar.ShowPopup();
        }
    }

    public void TukarBibitCommon()
    {
        if (inventoryManager.GetBibitCount() >= 1)
        {
            inventoryManager.UseBibit();
            inventoryManager.AddKoinFantasy(500);
            UpdateExchangeCanvas();
        }
        else
        {
            tidakpunya.ShowPopup();
        }
    }

    public void TukarBibitRare()  // Fungsi untuk menukar BibitRare menjadi Koin Fantasy
    {
        if (inventoryManager.GetBibitRareCount() >= 1)
        {
            inventoryManager.UseBibitRare();  // Kurangi jumlah BibitRare
            inventoryManager.AddKoinFantasy(1500);  // Tambahkan 1500 Koin Fantasy
            UpdateExchangeCanvas();
        }
        else
        {
            tidakpunya.ShowPopup();
        }
    }

    public void TukarKoinkeRupiah()  // Fungsi untuk menukar Koin Fantasy menjadi Coin
    {
        if (inventoryManager.GetKoinFantasy() >= 1000)
        {
            inventoryManager.AddKoinFantasy(-1000);
            inventoryManager.AddCoin(1000000);  // Tambahkan Coin ke inventory

            UpiahManager.Instance.AddUpiah(1000000); // Tambahkan ke total koin di GameManager

            UpdateExchangeCanvas();
        }
        else
        {
            tidakcukuptukar.ShowPopup();
        }
    }

    public void UpgradeAttack()
    {
        if (!AttackLevel2 && !AttackLevel3)
        {
            if (inventoryManager.GetMonsterCount() >= 1 && inventoryManager.GetKoinFantasy() >= 500)
            {
                playerAttack.lv2 = true;
                AttackLevel2 = true;
                InfoApgred.AttackLvl2 = true;
                inventoryManager.AddMonster(-1);
                inventoryManager.AddKoinFantasy(-500);
                berhasildiupgrade.ShowPopup();

                UpdateExchangeCanvas();
            }
            else
            {
                tidakbisaupgrade.ShowPopup();
            }
        }
        else if(AttackLevel2 && !AttackLevel3)
        {
            if (inventoryManager.GetMonsterCount() >= 1 && inventoryManager.GetKoinFantasy() >= 1000)
            {
                inventoryManager.AddMonster(-1);
                inventoryManager.AddKoinFantasy(-1000);
                berhasildiupgrade.ShowPopup();
                playerAttack.lv3 = true;
                AttackLevel3 = true;
                InfoApgred.AttackLvl3 = true;

                UpdateExchangeCanvas();
            }
            else
            {
                tidakbisaupgrade.ShowPopup();
            }
        }
    }

    public void UpgradeSkillLevel()
    {
        if(playerSkill.skillLevel == 1)
        {
            if (inventoryManager.GetMonsterCount() >= 1 && inventoryManager.GetKoinFantasy() >= 500)
            {
                inventoryManager.AddMonster(-1);
                inventoryManager.AddKoinFantasy(-500);
                berhasildiupgrade.ShowPopup();
                playerSkill.SetSkillLevel(2);
                InfoApgred.SkillLvl2 = true;

                UpdateExchangeCanvas();
            }
            else
            {
                tidakbisaupgrade.ShowPopup();
            }
        }
        else if(playerSkill.skillLevel == 2)
        {
            if (inventoryManager.GetMonsterCount() >= 1 && inventoryManager.GetKoinFantasy() >= 1000)
            {
                inventoryManager.AddMonster(-1);
                inventoryManager.AddKoinFantasy(-1000);
                berhasildiupgrade.ShowPopup();
                playerSkill.SetSkillLevel(3);
                InfoApgred.SkillLvl3 = true;

                UpdateExchangeCanvas();
            }
            else
            {
                tidakbisaupgrade.ShowPopup();
            }
        }
    }

    public void UpgradeUltiLevel()
    {
        if (playerUlti.currentLevel == 1 && !playerUlti.lv2)
        {
            if (inventoryManager.GetMonsterCount() >= 1 && inventoryManager.GetKoinFantasy() >= 500)
            {
                inventoryManager.AddMonster(-1);
                inventoryManager.AddKoinFantasy(-500);
                berhasildiupgrade.ShowPopup();
                playerUlti.SetSkillLevel(2);
                InfoApgred.UltiLvl2 = true;

                UpdateExchangeCanvas();
            }
            else
            {
                tidakbisaupgrade.ShowPopup();
            }
        }
        else if(playerUlti.currentLevel == 2 && playerUlti.lv2)
        {
            if (inventoryManager.GetMonsterCount() >= 1 && inventoryManager.GetKoinFantasy() >= 1000)
            {
                inventoryManager.AddMonster(-1);
                inventoryManager.AddKoinFantasy(-1000);
                berhasildiupgrade.ShowPopup(); playerUlti.SetSkillLevel(3);
                InfoApgred.UltiLvl3 = true;

                UpdateExchangeCanvas();
            }
            else
            {
                tidakbisaupgrade.ShowPopup();
            }
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
            buka.HideUI();
            playerMovementScript.enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
