using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Damage Serangan")]
    public int Damage;
    public int Damage2;
    public int Damage3;

    [Header("Level Serangan")]
    public bool lv2;
    public bool lv3;

    [Header("Animator Serangan")]
    public Animator animator;
    private ThirdPersonController Gerak;  // Referensi ke skrip pergerakan pemain
    public bool sedangMenyerang;

    [Header("VFX")]
    public GameObject vfxPrefab1;  // Prefab VFX 1
    public Transform vfxSpawnPoint1;  // Posisi di mana VFX 1 akan muncul
    public float vfxLifetime1 = 2f;  // Waktu sebelum VFX 1 dihancurkan

    [Header("Delay Serangan")]
    public float attackDelay = 0.5f; // Durasi sebelum gerakan kembali diaktifkan

    void Start()
    {
        animator = GetComponent<Animator>();
        Gerak = GetComponent<ThirdPersonController>();
        sedangMenyerang = false; // Set awal bahwa karakter tidak sedang menyerang
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !sedangMenyerang)
        {
            StartCoroutine(Serang());
        }

        if (lv2 && !lv3)
        {
            Damage = Damage2;
        }
        else if (lv3)
        {
            Damage = Damage3;
        }
    }

    IEnumerator Serang()
    {
        sedangMenyerang = true;
        Gerak.enabled = false;
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(attackDelay);

        Gerak.enabled = true;
        sedangMenyerang = false;
    }

    public void SpawnattackVFX1()
    {
        if (vfxPrefab1 != null && vfxSpawnPoint1 != null)
        {
            // Spawn VFX 1
            GameObject vfxInstance1 = Instantiate(vfxPrefab1, vfxSpawnPoint1.position, vfxSpawnPoint1.rotation);
            // Hancurkan VFX 1 setelah beberapa detik
            Destroy(vfxInstance1, vfxLifetime1);
        }
        else
        {
            Debug.LogWarning("VFX Prefab 1 atau VFX Spawn Point 1 belum diatur!");
        }
    }

    public void Idle()
    {
        animator.SetTrigger("Idle");
    }
}
