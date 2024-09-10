using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("Darah")]
    [SerializeField] private int DarahSekarang;
    [SerializeField] private int MaxDarah;

    [Header("Animator")]
    public Animator anim;

    [Header("Info")]
    public TMP_Text Min;
    public TMP_Text Max;

    [Header("Script")]
    public HealthBar Script;
    private void Awake()
    {
        MaxDarah = DarahSekarang;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            KenaDamage(5);
        }
        Min.text = DarahSekarang.ToString();
        Max.text = MaxDarah.ToString();
        if (DarahSekarang <= 0)
        {
            Mati();
        }
    }

    public void KenaDamage(int Jumlah)
    {
        DarahSekarang -= Jumlah;
        anim.SetTrigger("Hit");

        Script.SetHealth(DarahSekarang, MaxDarah);
    }

    public void Mati()
    {
        anim.SetBool("Death", true);
        Debug.Log("Kamu Mati!");
    }
}
