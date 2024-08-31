using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("Darah")]
    [SerializeField] private int DarahSekarang;
    [SerializeField] private int MaxDarah;

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
    }

    public void KenaDamage(int Jumlah)
    {
        DarahSekarang -= Jumlah;

        Script.SetHealth(DarahSekarang, MaxDarah);
    }

    public void Mati()
    {
        /*anim.SetBool("Mati", true);*/
        Debug.Log("Kamu Mati!");
    }
}
