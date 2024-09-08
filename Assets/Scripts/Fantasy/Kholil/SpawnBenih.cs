using UnityEngine;

public class SpawnBenih : MonoBehaviour
{
    public GameObject seedPrefab; // Prefab benih
    public Transform[] spawnPoints; // Titik spawn untuk benih
    public float spawnDelay = 5f; // Delay sebelum benih baru muncul
    private float spawnTimer = 0f;
    private GameObject[] spawnedSeeds; // Array untuk menyimpan referensi benih yang di-spawn

    void Start()
    {
        // Inisialisasi array untuk menyimpan referensi benih
        spawnedSeeds = new GameObject[spawnPoints.Length];

        // Spawn 2 benih di awal
        for (int i = 0; i < spawnPoints.Length && i < 2; i++)
        {
            SpawnSeed(i);
        }
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnDelay)
        {
            TrySpawnNewSeed();
            spawnTimer = 0f; // Reset timer setelah spawn
        }
    }

    void TrySpawnNewSeed()
    {
        // Cari titik spawn yang belum terisi benih
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnedSeeds[i] == null)
            {
                SpawnSeed(i);
                break; // Spawn hanya satu benih setiap kali
            }
        }
    }

    void SpawnSeed(int spawnIndex)
    {
        if (spawnPoints.Length > spawnIndex && spawnedSeeds[spawnIndex] == null)
        {
            // Instansiasi benih pada titik spawn
            spawnedSeeds[spawnIndex] = Instantiate(seedPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
        }
    }

    // Method ini bisa dipanggil oleh player ketika mengambil benih
    public void CollectSeed(GameObject seed)
    {
        for (int i = 0; i < spawnedSeeds.Length; i++)
        {
            if (spawnedSeeds[i] == seed)
            {
                spawnedSeeds[i] = null; // Kosongkan referensi saat benih diambil
                Destroy(seed); // Hancurkan benih yang diambil
                break;
            }
        }
    }
}
