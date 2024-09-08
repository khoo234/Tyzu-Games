using StarterAssets;
using System.Collections;
using UnityEngine;

public class Ulti : MonoBehaviour
{
    public int Damage;
    public int Damage2;
    public int Damage3;
    public bool lv2;
    public bool lv3;
    public Animator animator;
    private ThirdPersonController playerMovement;  // Referensi ke skrip pergerakan pemain
    public float attackDelay = 0.5f; // Durasi delay setelah serangan sebelum pemain bisa bergerak lagi

    // VFX 1 Level 1
    public GameObject vfxPrefab1;  // Prefab VFX 1
    public float vfxLifetimeLevel1 = 2f;  // Waktu sebelum VFX 1 dihancurkan pada level 1
    public float stunDurationLevel1 = 2f;  // Durasi stun pada level 1
    public Vector3 vfx1Scale = Vector3.one;  // Skala yang dapat diatur untuk VFX 1

    // VFX 1 Level 2
    public float vfxLifetimeLevel2 = 4f;  // Waktu sebelum VFX 1 dihancurkan pada level 2
    public float stunDurationLevel2 = 4f;  // Durasi stun pada level 2

    // VFX 1 Level 3 (Tambahan untuk level 3)
    public float vfxLifetimeLevel3 = 6f;  // Waktu sebelum VFX 1 dihancurkan pada level 3
    public float stunDurationLevel3 = 6f;  // Durasi stun pada level 3

    // VFX 2
    public GameObject vfxPrefab2;  // Prefab VFX 2
    public Transform vfxSpawnPoint2;  // Posisi di mana VFX 2 akan muncul
    public float vfxLifetime2 = 2f;  // Waktu sebelum VFX 2 dihancurkan

    private Transform enemyTransform;  // Referensi ke Transform enemy (akan di-update secara dinamis)

    // Stun
    private GameObject stunVFX;  // VFX yang akan dimainkan saat musuh terkena jurus

    // Level
    private int currentLevel = 1;  // Level saat ini (hingga tiga level: 1, 2, dan 3)

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<ThirdPersonController>();
    }

    void Update()
    {
        // Cek apakah enemyTransform sudah di-set (berarti musuh sudah ditemukan)
        if (enemyTransform == null)
        {
            FindEnemy();  // Coba cari musuh secara dinamis
        }

        // Jika pemain menekan tombol C, aktifkan Ulti
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (enemyTransform != null)  // Pastikan musuh sudah ada
            {
                playerMovement.enabled = false; // Nonaktifkan pergerakan pemain
                animator.SetTrigger("Ulti");
                StartCoroutine(SpawnAndFollowVFX1());  // Panggil Coroutine untuk memunculkan dan mengikuti enemy
                StartCoroutine(ResetMovementAfterAttack(attackDelay));  // Gunakan delay yang dapat diatur

                // Aplikasikan stun dan damage ke musuh berdasarkan level
                ApplyStun();
            }
            else
            {
                Debug.LogWarning("Enemy belum ditemukan!");
            }
        }

        // Menambahkan logika untuk level 2 dan level 3
        if (currentLevel == 2 && !lv2)
        {
            lv2 = true;  // Tandai bahwa level 2 telah dicapai
            Damage = Damage2;
            Debug.Log("Skill Ulti LV2");
        }
        else if (currentLevel == 3 && lv2 && !lv3)
        {
            lv3 = true;  // Tandai bahwa level 3 telah dicapai
            Damage = Damage3;
            Debug.Log("Skill Ulti LV3");
        }
    }

    // Method to set the skill level
    public void SetSkillLevel(int level)
    {
        // Validate and set the skill level
        if (level >= 1 && level <= 3)
        {
            currentLevel = level;
            Debug.Log("Skill level set to: " + currentLevel);

            // Update the damage and other properties based on the level
            if (currentLevel == 2 && !lv2)
            {
                lv2 = true;  // Tandai bahwa level 2 telah dicapai
                Damage = Damage2;
                Debug.Log("Skill Ulti LV2");
            }
            else if (currentLevel == 3 && lv2 && !lv3)
            {
                lv3 = true;  // Tandai bahwa level 3 telah dicapai
                Damage = Damage3;
                Debug.Log("Skill Ulti LV3");
            }
        }
        else
        {
            Debug.LogWarning("Invalid skill level: " + level);
        }
    }

    // Coroutine untuk spawn VFX 1 dan mengikutinya pada enemy
    IEnumerator SpawnAndFollowVFX1()
    {
        if (vfxPrefab1 != null && enemyTransform != null)
        {
            GameObject vfxInstance1 = Instantiate(vfxPrefab1, enemyTransform.position, Quaternion.identity);
            vfxInstance1.transform.localScale = vfx1Scale;

            // Tentukan lifetime berdasarkan level
            float vfxLifetime = (currentLevel == 1) ? vfxLifetimeLevel1 : (currentLevel == 2) ? vfxLifetimeLevel2 : vfxLifetimeLevel3;

            // Selama durasi hidup VFX, buat VFX mengikuti posisi musuh
            float timer = 0f;
            while (timer < vfxLifetime)
            {
                if (enemyTransform != null)  // Cek apakah musuh masih ada
                {
                    vfxInstance1.transform.position = enemyTransform.position;  // VFX mengikuti enemy
                }
                timer += Time.deltaTime;
                yield return null;
            }

            Destroy(vfxInstance1);  // Hancurkan VFX setelah waktu selesai
        }
        else
        {
            Debug.LogWarning("VFX Prefab 1 atau enemyTransform belum diatur!");
        }
    }

    // Mencari musuh secara dinamis berdasarkan tag
    void FindEnemy()
    {
        GameObject enemyObject = GameObject.FindWithTag("Enemy");
        if (enemyObject != null)
        {
            enemyTransform = enemyObject.transform;
            Debug.Log("Enemy ditemukan dan enemyTransform di-set.");
        }
        else
        {
            Debug.LogWarning("Enemy belum ditemukan!");
        }
    }

    // Set enemyTransform secara manual dari luar (misalnya saat enemy di-spawn)
    public void SetEnemyTransform(Transform newEnemyTransform)
    {
        enemyTransform = newEnemyTransform;
    }

    // Memunculkan VFX 2 (jika dibutuhkan)
    void SpawnVFX2()
    {
        if (vfxPrefab2 != null && vfxSpawnPoint2 != null)
        {
            GameObject vfxInstance2 = Instantiate(vfxPrefab2, vfxSpawnPoint2.position, vfxSpawnPoint2.rotation);
            Destroy(vfxInstance2, vfxLifetime2);
        }
        else
        {
            Debug.LogWarning("VFX Prefab 2 atau VFX Spawn Point 2 belum diatur!");
        }
    }

    // Coroutine untuk mengaktifkan kembali pergerakan pemain setelah delay
    IEnumerator ResetMovementAfterAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerMovement.enabled = true; // Aktifkan kembali pergerakan pemain
    }

    // Fungsi untuk menerapkan stun ke musuh
    void ApplyStun()
    {
        if (enemyTransform != null)
        {
            // Mainkan VFX stun jika ada
            if (stunVFX != null)
            {
                Instantiate(stunVFX, enemyTransform.position, Quaternion.identity);
            }

            // Tentukan stun duration berdasarkan level
            float stunDuration = (currentLevel == 1) ? stunDurationLevel1 : (currentLevel == 2) ? stunDurationLevel2 : stunDurationLevel3;

            // Cegah musuh bergerak dengan memberikan stun
            AIFollowPlayer enemyAI = enemyTransform.GetComponent<AIFollowPlayer>();
            if (enemyAI != null)
            {
                enemyAI.Stun(stunDuration);  // Stun musuh dengan durasi yang telah diatur
            }
        }
        else
        {
            Debug.LogWarning("Enemy belum ditemukan untuk diterapkan stun!");
        }
    }
}
