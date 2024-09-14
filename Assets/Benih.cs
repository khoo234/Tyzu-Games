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
    public float searchRadius = 2f;

    [Header("Event")]
    public UnityEvent Setengah;

    [Header("Enemy Settings")]
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;

    [Header("Event Sekali")]
    public UnityEvent Sekali;

    private MenanamBenih Ladang;

    public bool Panen;
    public bool Ditanam = false;
    public bool isWatered = false;
    public bool canBeDestroyed = false;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Start()
    {
        Text.text = NamaObject;
        WaktuText.gameObject.SetActive(false);

        GameObject[] spawnEnemyObjects = GameObject.FindGameObjectsWithTag("SpawnEnemy");
        spawnPoints = new Transform[spawnEnemyObjects.Length];

        for (int i = 0; i < spawnEnemyObjects.Length; i++)
        {
            spawnPoints[i] = spawnEnemyObjects[i].transform;
        }
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
                Biji.SetActive(false);
                SudahSelesai.SetActive(true);
                Panen = true;
                Text.text = NamaObject2;
                canBeDestroyed = true;
                Ladang.plantedSeed = null;
                Ladang.hasPlantedSeed = false;
            }

            if (waktuSisa == 10 && spawnedEnemies.Count == 0)
            {
                SpawnEnemies();
                timerPaused = true;
                if(Ladang.Dialog != null)
                {
                    Ladang.Dialog.StartDialog();
                }
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
                if(Ladang.Dialog != null)
                {
                    Ladang.Dialog.StartDialog();
                }
                timerPaused = false;
            }
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
        foreach (var collider in colliders)
        {
            MenanamBenih ladang = collider.GetComponent<MenanamBenih>();
            if (ladang != null)
            {
                Ladang = ladang;
                break;
            }
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

            if (CompareTag("SeedRare"))
            {
                InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
                if (inventoryManager != null && inventoryManager.HasPupuk())
                {
                    inventoryManager.UsePupuk(); // Mengurangi pupuk
                }
                else
                {
                    Debug.Log("Tidak ada pupuk yang cukup untuk benih rare.");
                    return; // Keluar jika tidak ada pupuk
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
            Panen = false;
            Destroy(gameObject);
        }
    }

    public void AddBibitToInventory()
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager != null)
        {
            if (CompareTag("SeedRare"))
            {
                inventoryManager.AddBibitRare(bibitValue);
            }
            else
            {
                inventoryManager.AddBibit(bibitValue);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Ditanam)
        {
            if (other.CompareTag("Player"))
            {
                InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
                if (inventoryManager != null)
                {
                    if (CompareTag("SeedRare"))
                    {
                        inventoryManager.AddSeedRare(benihValue); // Menambahkan benih rare ke inventory
                        Sekali?.Invoke();
                    }
                    else
                    {
                        inventoryManager.AddSeed(benihValue); // Menambahkan benih biasa ke inventory
                        Sekali?.Invoke();
                    }
                }

                Destroy(gameObject);
            }
        }
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
