using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator animator;
    private ThirdPersonController playerMovement;  // Referensi ke skrip pergerakan pemain

    // VFX 1
    public GameObject vfxPrefab1;  // Prefab VFX 1
    public Transform vfxSpawnPoint1;  // Posisi di mana VFX 1 akan muncul
    public float vfxLifetime1 = 2f;  // Waktu sebelum VFX 1 dihancurkan

    // Durasi delay setelah serangan sebelum pemain bisa bergerak lagi
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
    }

    void SpawnattackVFX1()
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
