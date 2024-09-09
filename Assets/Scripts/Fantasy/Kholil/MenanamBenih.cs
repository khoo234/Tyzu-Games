using UnityEngine;

public class MenanamBenih : MonoBehaviour
{
    public GameObject seedPrefab;       // Prefab untuk benih biasa
    public GameObject seedRarePrefab;   // Prefab untuk benih rare
    public GameObject waterVFXPrefab;
    public GameObject fertilizerPrefab; // Prefab untuk pupuk
    public bool canPlant = false;
    public bool isSeedRareField = false; // Menandakan apakah ladang ini untuk benih rare

    private Benih plantedSeed;
    private bool hasPlantedSeed = false; // Flag untuk mengecek apakah sudah menanam benih
    private bool isFertilized = false; // Flag untuk mengecek apakah pupuk sudah digunakan

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPlant && !hasPlantedSeed)
        {
            PlantSeed();
        }

        if (Input.GetKeyDown(KeyCode.Q) && canPlant && isSeedRareField && plantedSeed != null)
        {
            Debug.Log("Memberi pupuk");
            UseFertilizer();
        }

        if (Input.GetKeyDown(KeyCode.F) && canPlant && plantedSeed != null)
        {
            if (isSeedRareField && !isFertilized)
            {
                Debug.Log("Harus memberi pupuk terlebih dahulu.");
                return;
            }

            Debug.Log("Menyiram");
            WaterSeed();
        }
    }

    void PlantSeed()
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager != null)
        {
            GameObject seedToPlant = null;

            // Pilih prefab benih yang sesuai dengan jenis ladang
            if (isSeedRareField && inventoryManager.HasSeedRare())
            {
                seedToPlant = seedRarePrefab;
                inventoryManager.UseSeedRare(); // Mengurangi jumlah benih rare dari inventory
            }
            else if (!isSeedRareField && inventoryManager.HasSeeds())
            {
                seedToPlant = seedPrefab;
                inventoryManager.UseSeed(); // Mengurangi jumlah benih biasa dari inventory
            }
            else
            {
                Debug.LogWarning("Tidak ada benih yang sesuai untuk ditanam.");
                return;
            }

            Vector3 plantPosition = transform.position + Vector3.up;
            Collider[] colliders = Physics.OverlapSphere(plantPosition, 0.1f);
            Benih existingSeed = null;

            foreach (Collider collider in colliders)
            {
                Benih benih = collider.GetComponent<Benih>();
                if (benih != null)
                {
                    existingSeed = benih;
                    break;
                }
            }

            if (existingSeed == null)
            {
                GameObject seed = Instantiate(seedToPlant, plantPosition, Quaternion.identity);

                Benih benihScript = seed.GetComponent<Benih>();
                if (benihScript != null)
                {
                    benihScript.Ditanam = true;
                    plantedSeed = benihScript;
                    hasPlantedSeed = true; // Menandai bahwa benih sudah ditanam

                    Collider seedCollider = seed.GetComponent<Collider>();
                    if (seedCollider != null)
                    {
                        seedCollider.enabled = false;
                    }

                    Debug.Log("Benih berhasil ditanam.");
                }
            }
            else
            {
                Debug.LogWarning("Benih sudah ada di posisi ini.");
            }
        }
        else
        {
            Debug.LogWarning("InventoryManager tidak ditemukan");
        }
    }

    void WaterSeed()
    {
        if (plantedSeed != null)
        {
            GameObject waterVFX = Instantiate(waterVFXPrefab, transform.position, Quaternion.identity);
            Destroy(waterVFX, 2f);

            plantedSeed.StartWatering();
            plantedSeed = null; // Reset plantedSeed setelah menyiram
            hasPlantedSeed = false; // Reset flag setelah menyiram
            isFertilized = false; // Reset status pupuk setelah menyiram
        }
    }

    void UseFertilizer()
    {
        if (plantedSeed != null)
        {
            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            if (inventoryManager != null && inventoryManager.HasPupuk())
            {
                inventoryManager.UsePupuk(); // Mengurangi pupuk dari inventory
                isFertilized = true; // Tandai bahwa pupuk sudah digunakan
                Debug.Log("Pupuk digunakan.");

                // Menampilkan efek pupuk sedikit ke atas dari posisi tanaman
                Vector3 fertilizerPosition = transform.position + Vector3.up * 1.0f; // Ubah 1.0f sesuai dengan offset yang diinginkan
                GameObject fertilizerVFX = Instantiate(fertilizerPrefab, fertilizerPosition, Quaternion.identity);
                Destroy(fertilizerVFX, 2f); // Hapus efek setelah 2 detik
            }
            else
            {
                Debug.LogWarning("Pupuk tidak cukup.");
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlant = true;
            Debug.Log("Player berada di area tanam.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlant = false;
            Debug.Log("Player keluar dari area tanam.");
        }
    }
}
