using System.Collections.Generic; // Tambahkan ini untuk List
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Benih : MonoBehaviour
{
    [Header("Value")]
    public int benihValue = 1;
    public int bibitValue = 1; // Tambahkan variabel untuk bibit

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

    // List untuk menyimpan musuh yang di-spawn
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

            if (waktuSisa == 20 && spawnedEnemies.Count == 0) // Cek apakah belum ada musuh
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
            // Cek apakah semua musuh sudah mati
            bool allEnemiesDead = true;
            foreach (GameObject enemy in spawnedEnemies)
            {
                if (enemy != null) // Pastikan musuh masih ada
                {
                    Darah enemyHealth = enemy.GetComponent<Darah>();
                    if (enemyHealth != null && !enemyHealth.isDead)
                    {
                        allEnemiesDead = false;
                        break; // Keluar dari loop jika ada musuh yang belum mati
                    }
                }
            }

            if (allEnemiesDead)
            {
                timerPaused = false; // Lanjutkan waktu jika semua musuh mati
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
            // Pilih salah satu spawn point secara acak
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Pilih salah satu prefab musuh secara acak
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // Instansiasi musuh di posisi spawn point yang dipilih
            GameObject enemy = Instantiate(randomEnemyPrefab, spawnPoint.position, Quaternion.identity);

            // Masukkan musuh ke dalam list
            spawnedEnemies.Add(enemy);

            // Dapatkan komponen 'Darah' dari musuh dan set referensi ke script 'Benih'
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
            inventoryManager.AddBibit(bibitValue); // Menambah bibit ke inventory
            Debug.Log("Bibit ditambahkan ke inventory.");
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
                inventoryManager.AddSeed(benihValue);
            }

            Destroy(gameObject);
        }
    }
}
