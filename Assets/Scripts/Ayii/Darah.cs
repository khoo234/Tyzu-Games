using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darah : MonoBehaviour
{
    [Header("Darah")]
    [SerializeField] private int DarahSekarang;
    [SerializeField] private int MaxDarah;

    public HealthBar Script;
    private void Awake()
    {
        MaxDarah = DarahSekarang;
    }

    void Start()
    {
        Script.SetHealth(DarahSekarang, MaxDarah);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
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
        float destroyDelay = Random.value;
        Destroy(gameObject, destroyDelay);
    }
}
