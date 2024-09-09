using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Benih : MonoBehaviour
{
    [Header("Value")]
    public int benihValue = 1;
    public int bibitValue = 1;

    [Header("Menampilkan Informasi Object")]
    public string NamaObject;
    public string NamaObject2;
    public TMP_Text Text;

    [Header("Tampilkan Waktu")]
    public TMP_Text WaktuText;
    public int waktuDurasi = 30;
    private float waktuBerjalan = 0f;
    private bool waktuAktif = false;
    private bool timerPaused = false;

    [Header("Bentuk Benih")]
    public GameObject Biji;
    public GameObject SudahSelesai;

    [Header("Event")]
    public UnityEvent Setengah;

    [Header("Enemy Settings")]
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;

    public bool Ditanam = false;
    private bool isWatered = false;
    private bool canBeDestroyed = false;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Start()
    {
        Text.text = NamaObject;
        WaktuText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (waktuAktif && !timerPaused)
        {
            waktuBerjalan += Time.deltaTime;
            int waktuSisa = Mathf.CeilToInt(waktuDurasi - waktuBerjalan);
            WaktuText.text = waktuSisa.ToString();

            if (waktuSisa <= 0)
            {
                waktuAktif = false;
                WaktuText.gameObject.SetActive(false);
                Debug.Log("Waktu telah tercapai!");
                Biji.SetActive(false);
                SudahSelesai.SetActive(true);
                Text.text = NamaObject2;
                canBeDestroyed = true;

                // Tambahkan bibit ke inventory
                AddBibitToInventory();
            }

            if (waktuSisa == 10 && spawnedEnemies.Count == 0)
            {
                SpawnEnemies();
                timerPaused = true;
            }

            if (waktuSisa == 5)
            {
                Setengah?.Invoke();
            }
        }

        if (timerPaused)
        {
            bool allEnemiesDead = true;
            foreach (GameObject enemy in spawnedEnemies)
            {
                if (enemy != null)
                {
                    Darah enemyHealth = enemy.GetComponent<Darah>();
                    if (enemyHealth != null && !enemyHealth.isDead)
                    {
                        allEnemiesDead = false;
                        break;
                    }
                }
            }

            if (allEnemiesDead)
            {
                timerPaused = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && canBeDestroyed)
        {
            DestroySeed();
        }
    }

    private void SpawnEnemies()
    {
        if (spawnPoints.Length > 0 && enemyPrefabs.Length > 0)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject enemy = Instantiate(randomEnemyPrefab, spawnPoint.position, Quaternion.identity);
            spawnedEnemies.Add(enemy);

            Darah enemyHealth = enemy.GetComponent<Darah>();
            if (enemyHealth != null)
            {
                enemyHealth.SetBenih(this);
            }
        }
    }

    public void StartWatering()
    {
        if (!isWatered)
        {
            isWatered = true;
            ReceiveWatering();

            // Pastikan benih rare dikurangi setiap kali benih rare ditanam
            if (CompareTag("SeedRare"))
            {
                InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
                if (inventoryManager != null && inventoryManager.HasSeedRare())
                {
                    inventoryManager.UseSeedRare(); // Mengurangi benih rare
                    Debug.Log("Benih rare ditanam, jumlah berkurang.");
                }
            }
        }
    }


    private void ReceiveWatering()
    {
        if (Ditanam)
        {
            WaktuText.gameObject.SetActive(true);
            waktuBerjalan = 0f;
            waktuAktif = true;
            WaktuText.text = waktuDurasi.ToString();
        }
    }

    public bool CanBeDestroyed()
    {
        return canBeDestroyed;
    }

    public void DestroySeed()
    {
        if (canBeDestroyed)
        {
            Debug.Log("Destroying seed.");
            Destroy(gameObject);
        }
    }

    private void AddBibitToInventory()
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager != null)
        {
            if (CompareTag("SeedRare"))
            {
                inventoryManager.AddBibitRare(bibitValue);
                Debug.Log("Bibit Rare ditambahkan ke inventory.");
            }
            else
            {
                inventoryManager.AddBibit(bibitValue);
                Debug.Log("Bibit ditambahkan ke inventory.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Benih collected! Value: " + benihValue);

            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            if (inventoryManager != null)
            {
                if (CompareTag("SeedRare"))
                {
                    inventoryManager.AddSeedRare(benihValue); // Menambahkan benih rare ke inventory
                }
                else
                {
                    inventoryManager.AddSeed(benihValue); // Menambahkan benih biasa ke inventory
                }
            }

            Destroy(gameObject);
        }
    }
}
