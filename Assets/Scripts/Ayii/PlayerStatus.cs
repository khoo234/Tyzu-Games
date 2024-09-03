using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("Darah")]
    [SerializeField] private int DarahSekarang;
    [SerializeField] private int MaxDarah;

    [Header ("Attack Information")]
    [SerializeField] private int Damage;

    [Header("Animator")]
    public Animator anim;

    [Header ("Info UI")]
    public TMP_Text Min;
    public TMP_Text Max;

    public HealthBar Script;
    private void Awake()
    {
        MaxDarah = DarahSekarang;
        Max.text = MaxDarah.ToString();
        Min.text = DarahSekarang.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            KenaDamage(10);
        }
        if (DarahSekarang <= 0)
        {
            Mati();
        }
        
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
    }

    public void KenaDamage(int Jumlah)
    {
        DarahSekarang -= Jumlah;
        Min.text = DarahSekarang.ToString();
        Script.SetHealth(DarahSekarang, MaxDarah);
    }

    public void Mati()
    {
        /*anim.SetBool("Mati", true);*/
        Debug.Log("Kamu Mati!");
    }
}
