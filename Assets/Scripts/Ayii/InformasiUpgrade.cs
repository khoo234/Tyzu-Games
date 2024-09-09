using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InformasiUpgrade : MonoBehaviour
{
    [Header ("Menampilkan")]
    public TMP_Text Judul;
    public TMP_Text Informasi;

    [Header ("Attack Info")]
    [SerializeField] private string Attack2;
    [SerializeField] private string InformasiAttack2;
    public bool AttackLvl2;

    [SerializeField] private string Attack3;
    [SerializeField] private string InformasiAttack3;
    public bool AttackLvl3;

    [SerializeField] private GameObject GambarAttack;
    [SerializeField] private GameObject AttackUpgrade;
    [SerializeField] private Button TombolAttackUpgrade;
    [SerializeField] private TMP_Text AttackLevel;
    [SerializeField] private GameObject AttackLevelText;

    [Header ("Skill Info")]
    [SerializeField] private string Skill2;
    [SerializeField] private string InformasiSkill2;
    public bool SkillLvl2;

    [SerializeField] private string Skill3;
    [SerializeField] private string InformasiSkill3;
    public bool SkillLvl3;
    [SerializeField] private GameObject GambarSkill;
    [SerializeField] private GameObject SkillUpgrade;
    [SerializeField] private Button TombolSkillUpgrade;
    [SerializeField] private TMP_Text SkillLevel;
    [SerializeField] private GameObject SkillLevelText;

    [Header("Ulti Info")]
    [SerializeField] private string Ulti2;
    [SerializeField] private string InformasiUlti2;
    public bool UltiLvl2;

    [SerializeField] private string Ulti3;
    [SerializeField] private string InformasiUlti3;
    public bool UltiLvl3;
    [SerializeField] private GameObject GambarUlti;
    [SerializeField] private GameObject UltiUpgrade;
    [SerializeField] private Button TombolUltiUpgrade;
    [SerializeField] private TMP_Text UltiLevel;
    [SerializeField] private GameObject UltiLevelText;

    [Header("Script")]
    public InventoryManager Script1;

    private void Start()
    {
        Item1();
        Script1 = FindAnyObjectByType<InventoryManager>();
    }

    private void Update()
    {
        // Attack
        if(AttackLvl2 && AttackLvl3)
        {
            TombolAttackUpgrade.interactable = false;
            AttackLevel.text = "Max";
        }
        else if(AttackLvl2 && !AttackLvl3)
        {
            AttackLevel.text = "Level 3";
        }

        // Skill
        if (SkillLvl2 && SkillLvl3)
        {
            TombolSkillUpgrade.interactable = false;
            SkillLevel.text = "Max";
        }
        else if (SkillLvl2 && !SkillLvl3)
        {
            SkillLevel.text = "Level 3";
        }

        // Ulti
        if (UltiLvl2 && UltiLvl3)
        {
            TombolUltiUpgrade.interactable = false;
            UltiLevel.text = "Max";
        }
        else if (UltiLvl2 && !UltiLvl3)
        {
            UltiLevel.text = "Level 3";
        }
    }

    public void Item1()
    {
        if (!AttackLvl2)
        {
            Judul.text = Attack2;
            Informasi.text = InformasiAttack2;
            AttackLevel.text = "Level 2";
        }
        else if (AttackLvl2)
        {
            Judul.text = Attack3;
            Informasi.text = InformasiAttack3;
            AttackLevel.text = "Max";
        }
        // Attack
        GambarAttack.SetActive(true);
        AttackUpgrade.SetActive (true);
        AttackLevelText.SetActive (true);
        // Skill
        SkillUpgrade.SetActive (false);
        GambarSkill.SetActive (false);
        SkillLevel.text = "";
        SkillLevelText.SetActive(false);
        // Ulti
        UltiUpgrade.SetActive(false);
        GambarUlti.SetActive(false);
        UltiLevel.text = "";
        UltiLevelText.SetActive(false);
    }
    public void Item2()
    {
        if (!SkillLvl2)
        {
            Judul.text = Skill2;
            Informasi.text = InformasiSkill2;
            SkillLevel.text = "Level 2";
        }
        else if (SkillLvl2)
        {
            Judul.text = Skill3;
            Informasi.text = InformasiSkill3;
            SkillLevel.text = "Max";
        }
        // Attack
        GambarAttack.SetActive(false);
        AttackUpgrade.SetActive(false);
        AttackLevelText.SetActive(false);
        AttackLevel.text = "";
        // Skill
        SkillUpgrade.SetActive(true);
        GambarSkill.SetActive(true);
        SkillLevelText.SetActive(true);
        // Ulti
        UltiUpgrade.SetActive(false);
        GambarUlti.SetActive(false);
        UltiLevel.text = "";
        UltiLevelText.SetActive(false);
    }

    public void Item3()
    {
        if (!UltiLvl2)
        {
            Judul.text = Ulti2;
            Informasi.text = InformasiUlti2;
            UltiLevel.text = "Level 2";
        }
        else if (UltiLvl2)
        {
            Judul.text = Ulti3;
            Informasi.text = InformasiUlti3;
            UltiLevel.text = "Max";
        }
        // Attack
        GambarAttack.SetActive(false);
        AttackUpgrade.SetActive(false);
        AttackLevelText.SetActive(false);
        AttackLevel.text = "";
        // Skill
        SkillUpgrade.SetActive(false);
        GambarSkill.SetActive(false);
        SkillLevel.text = "";
        SkillLevelText.SetActive(false);
        // Ulti
        UltiUpgrade.SetActive(true);
        GambarUlti.SetActive(true);
        UltiLevelText.SetActive(true);
    }
}
