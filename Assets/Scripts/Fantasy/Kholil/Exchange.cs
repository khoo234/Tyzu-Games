using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;

public class Exchange : MonoBehaviour
{
    [Header("Interact")]
    public GameObject TekanE;
    public AudioSource UpgradeAttacks;
    public AudioSource UpgradeSkill;
    public AudioSource UpgradeUlti;
    public AudioSource Sell;
    public AudioSource UangFantasy;

    [Header("Animasi")]
    public Animasi buka;
    public Notifikasi berhasilbeli;
    public Notifikasi berhasildiupgrade;
    public Notifikasi berhasiltukar;
    public Notifikasi tidakpunya;
    public Notifikasi tidakcukuptukar;
    public Notifikasi tidakbisaupgrade;
    public Notifikasi Sold;

    private bool isInTrigger = false;
    private bool bukatoko = false;

    [Header("Script")]
    public MonoBehaviour playerMovementScript;
    public Attack playerAttack;
    public Skill2 playerSkill;
    public Ulti playerUlti;
    public InventoryManager inventoryManager;
    public InformasiUpgrade InfoApgred;
    public UpiahManager Rupiah;

    [Header("Attack Level")]
    public bool AttackLevel2;
    public bool AttackLevel3;

    [Header("Skill Level")]
    public bool SkillLevel2;
    public bool SkillLevel3;

    [Header("Ulti Level")]
    public bool UltiLevel2;
    public bool UltiLevel3;

    [Header("Skin")]
    public bool BeliSkin;

    [Header("Text")]
    public TextMeshProUGUI TextBibitBiasa;
    public TextMeshProUGUI TextBibitRare;
    public TextMeshProUGUI TextKoin;
    public TextMeshProUGUI TextHama;
    public CharacterSwitcher characterSwitcher;

    [Header("Buka")]
    public UnityEvent bukatokoo;

    [Header("Tutup")]
    public UnityEvent tutuptokoo;


    void Start()
    {
        playerUlti = FindAnyObjectByType<Ulti>();
        playerSkill = FindAnyObjectByType<Skill2>();
        playerAttack = FindAnyObjectByType<Attack>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        LoadStatus();
    }

    void Update()
    {
        if (!bukatoko)
        {
            if (Input.GetKeyDown(KeyCode.E) && isInTrigger)
            {
                BukaToko();
                bukatoko = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E) && isInTrigger)
            {
                TutupToko();
                bukatoko = false;
            }
        }
        Rupiah = FindAnyObjectByType<UpiahManager>();

        UpdateUpgradeStatus();
    }

    private void UpdateUpgradeStatus()
    {
        // Attack
        if (AttackLevel2 && AttackLevel3)
        {
            playerAttack.lv3 = true;
            InfoApgred.AttackLvl2 = true;
            InfoApgred.AttackLvl3 = true;
        }
        else if (AttackLevel2)
        {
            playerAttack.lv2 = true;
            InfoApgred.AttackLvl2 = true;
        }

        // Skill
        if (SkillLevel2 && SkillLevel3)
        {
            playerSkill.SetSkillLevel(3);
            InfoApgred.SkillLvl2 = true;
            InfoApgred.SkillLvl3 = true;
        }
        else if (SkillLevel2)
        {
            playerSkill.SetSkillLevel(2);
            InfoApgred.SkillLvl2 = true;
        }

        // Ulti
        if (UltiLevel2 && UltiLevel3)
        {
            playerUlti.SetSkillLevel(3);
            InfoApgred.UltiLvl2 = true;
            InfoApgred.UltiLvl3 = true;
        }
        else if (UltiLevel2)
        {
            playerUlti.SetSkillLevel(2);
            InfoApgred.UltiLvl2 = true;
        }

        if (BeliSkin)
        {
            characterSwitcher.isKoinExchanged = true;
        }
        UpdateExchangeCanvas();
    }

    public void BukaToko()
    {
        playerMovementScript.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        buka.ShowUI();
        bukatokoo?.Invoke();

        UpdateExchangeCanvas();
    }

    public void TutupToko()
    {
        playerMovementScript.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        buka.HideUI();
        tutuptokoo?.Invoke();

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
            inventoryManager.AddKoinFantasy(-500);
            UpdateExchangeCanvas();
            berhasiltukar.ShowPopup();
            Sell.Play();
        }
        else
        {
            tidakcukuptukar.ShowPopup();
        }
    }
    public void TukarKoinkeSkin()
    {
        if (!BeliSkin)
        {
            if (inventoryManager.GetKoinFantasy() >= 10000)
            {
                characterSwitcher.switchEffectObject.SetActive(true);
                characterSwitcher.SwitchToMaterialKucing2(characterSwitcher.badanPartName007);
                characterSwitcher.SwitchToMaterialBadan002_2();
                inventoryManager.AddKoinFantasy(-10000);
                berhasiltukar.ShowPopup();
                UpdateExchangeCanvas();
                Sell.Play();
                BeliSkin = true;
            }
            else
            {
                tidakcukuptukar.ShowPopup();
            }
        }
        else
        {
            Sold.ShowPopup();
        }
        SaveStatus();
        UpdateUpgradeStatus();
    }

    public void TukarBibitCommon()
    {
        if (inventoryManager.GetBibitCount() >= 1)
        {
            inventoryManager.UseBibit();
            inventoryManager.AddKoinFantasy(500);
            UpdateExchangeCanvas();
            berhasilbeli.ShowPopup();
            Sell.Play();
        }
        else
        {
            tidakpunya.ShowPopup();
        }
    }

    public void TukarBibitRare()
    {
        if (inventoryManager.GetBibitRareCount() >= 1)
        {
            inventoryManager.UseBibitRare();
            inventoryManager.AddKoinFantasy(2000);
            UpdateExchangeCanvas();
            berhasilbeli.ShowPopup();
            Sell.Play();
        }
        else
        {
            tidakpunya.ShowPopup();
        }
    }

    public void TukarKoinkeRupiah()
    {
        if (inventoryManager.GetKoinFantasy() >= 1000)
        {
            inventoryManager.AddKoinFantasy(-1000);
            Rupiah.AddUpiah(1000000);
            berhasiltukar.ShowPopup();
            UpdateExchangeCanvas();
            UangFantasy.Play();
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
                AttackLevel2 = true;
                inventoryManager.AddMonster(-1);
                inventoryManager.AddKoinFantasy(-500);
                berhasildiupgrade.ShowPopup();
                UpgradeAttacks.Play();
                UpdateExchangeCanvas();
            }
            else
            {
                tidakbisaupgrade.ShowPopup();
            }
        }
        else if (AttackLevel2 && !AttackLevel3)
        {
            if (inventoryManager.GetMonsterCount() >= 1 && inventoryManager.GetKoinFantasy() >= 1000)
            {
                inventoryManager.AddMonster(-1);
                inventoryManager.AddKoinFantasy(-1000);
                berhasildiupgrade.ShowPopup();
                AttackLevel3 = true;
                UpgradeAttacks.Play();
                UpdateExchangeCanvas();
            }
            else
            {
                tidakbisaupgrade.ShowPopup();
            }
        }
        SaveStatus();
        UpdateUpgradeStatus();
    }

    public void UpgradeSkillLevel()
    {
        if (playerSkill.skillLevel == 1)
        {
            if (inventoryManager.GetMonsterCount() >= 1 && inventoryManager.GetKoinFantasy() >= 700)
            {
                SkillLevel2 = true;
                inventoryManager.AddMonster(-1);
                inventoryManager.AddKoinFantasy(-700);
                berhasildiupgrade.ShowPopup();
                UpgradeSkill.Play();
                UpdateExchangeCanvas();
            }
            else
            {
                tidakbisaupgrade.ShowPopup();
            }
        }
        else if (playerSkill.skillLevel == 2)
        {
            if (inventoryManager.GetMonsterCount() >= 1 && inventoryManager.GetKoinFantasy() >= 1400)
            {
                SkillLevel3 = true;
                inventoryManager.AddMonster(-1);
                inventoryManager.AddKoinFantasy(-1400);
                berhasildiupgrade.ShowPopup();
                UpgradeSkill.Play();
                UpdateExchangeCanvas();
            }
            else
            {
                tidakbisaupgrade.ShowPopup();
            }
        }
        SaveStatus();
        UpdateUpgradeStatus();
    }

    public void UpgradeUltiLevel()
    {
        if (playerUlti.currentLevel == 1 && !playerUlti.lv2)
        {
            if (inventoryManager.GetMonsterCount() >= 1 && inventoryManager.GetKoinFantasy() >= 1000)
            {
                UltiLevel2 = true;
                inventoryManager.AddMonster(-1);
                inventoryManager.AddKoinFantasy(-1000);
                berhasildiupgrade.ShowPopup();
                UpgradeUlti.Play();
                UpdateExchangeCanvas();
            }
            else
            {
                tidakbisaupgrade.ShowPopup();
            }
        }
        else if (playerUlti.currentLevel == 2 && !playerUlti.lv3)
        {
            if (inventoryManager.GetMonsterCount() >= 1 && inventoryManager.GetKoinFantasy() >= 2000)
            {
                UltiLevel3 = true;
                inventoryManager.AddMonster(-1);
                inventoryManager.AddKoinFantasy(-2000);
                berhasildiupgrade.ShowPopup();
                playerUlti.SetSkillLevel(3);
                InfoApgred.UltiLvl3 = true;
                UpgradeUlti.Play();
                UpdateExchangeCanvas();
            }
            else
            {
                tidakbisaupgrade.ShowPopup();
            }
        }
        SaveStatus();
        UpdateUpgradeStatus();
    }

    private void SaveStatus()
    {
        PlayerPrefs.SetInt("AttackLevel2", AttackLevel2 ? 1 : 0);
        PlayerPrefs.SetInt("AttackLevel3", AttackLevel3 ? 1 : 0);
        PlayerPrefs.SetInt("SkillLevel2", SkillLevel2 ? 1 : 0);
        PlayerPrefs.SetInt("SkillLevel3", SkillLevel3 ? 1 : 0);
        PlayerPrefs.SetInt("UltiLevel2", UltiLevel2 ? 1 : 0);
        PlayerPrefs.SetInt("UltiLevel3", UltiLevel3 ? 1 : 0);
        PlayerPrefs.SetInt("BeliSkin", BeliSkin ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadStatus()
    {
        AttackLevel2 = PlayerPrefs.GetInt("AttackLevel2") == 1;
        AttackLevel3 = PlayerPrefs.GetInt("AttackLevel3") == 1;
        SkillLevel2 = PlayerPrefs.GetInt("SkillLevel2") == 1;
        SkillLevel3 = PlayerPrefs.GetInt("SkillLevel3") == 1;
        UltiLevel2 = PlayerPrefs.GetInt("UltiLevel2") == 1;
        UltiLevel3 = PlayerPrefs.GetInt("UltiLevel3") == 1;
        BeliSkin = PlayerPrefs.GetInt("BeliSkin") == 1;
    }

    public void ResetData()
    {
        AttackLevel2 = false;
        AttackLevel3 = false;
        SkillLevel2 = false;
        SkillLevel3 = false;
        UltiLevel2 = false;
        UltiLevel3 = false;
        BeliSkin = false;

        UpdateUpgradeStatus();
        UpdateExchangeCanvas();
        SaveStatus();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TekanE.SetActive(true);
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TekanE.SetActive(false);
            isInTrigger = false;
        }
    }
}
