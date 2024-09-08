using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("Darah")]
    [SerializeField] private int DarahSekarang;
    [SerializeField] private int MaxDarah;

    [Header ("Attack Information")]
    public int Damage;
    public int Damage2;
    public bool lv2;

    [Header("Animator")]
    public Animator anim;

    public HealthBar Script;
    private void Awake()
    {
        MaxDarah = DarahSekarang;
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
            anim.SetTrigger("Ulti");
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
