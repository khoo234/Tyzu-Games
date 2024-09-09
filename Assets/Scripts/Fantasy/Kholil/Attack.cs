using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header ("Damage Serangan")]
    public int Damage;
    public int Damage2;
    public int Damage3;

    [Header("Level Serangan")]
    public bool lv2;
    public bool lv3;

    [Header("Animator Serangan")]
    public Animator animator;
    private ThirdPersonController playerMovement;  // Referensi ke skrip pergerakan pemain

    [Header("VFX")]
    public GameObject vfxPrefab1;  // Prefab VFX 1
    public Transform vfxSpawnPoint1;  // Posisi di mana VFX 1 akan muncul
    public float vfxLifetime1 = 2f;  // Waktu sebelum VFX 1 dihancurkan

    [Header("Delay Serangan")]
    public float attackDelay = 0.5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        // Dapatkan referensi ke skrip ThirdPersonController
        playerMovement = GetComponent<ThirdPersonController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerMovement.enabled = false; // Nonaktifkan pergerakan pemain
            animator.SetTrigger("Attack");
            StartCoroutine(ResetMovementAfterAttack(attackDelay));  // Gunakan delay yang dapat diatur
        }
        if (lv2 && !lv3)
        {
            Damage = Damage2;  // Level 2 aktif, ganti damage
        }
        else if (lv3)
        {
            Damage = Damage3;  // Level 3 aktif, ganti damage
        }

    }

    // Dipanggil oleh Animation Event
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

    IEnumerator ResetMovementAfterAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerMovement.enabled = true; // Aktifkan kembali pergerakan pemain
    }
}