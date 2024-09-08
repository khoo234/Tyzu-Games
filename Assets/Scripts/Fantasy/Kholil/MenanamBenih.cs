using UnityEngine;

public class MenanamBenih : MonoBehaviour
{
    public GameObject seedPrefab;
    public GameObject waterVFXPrefab;
    public bool canPlant = false;

    private Benih plantedSeed;
    private bool hasPlantedSeed = false; // Flag untuk mengecek apakah sudah menanam benih

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPlant && !hasPlantedSeed)
        {
            PlantSeed();
        }

        if (Input.GetKeyDown(KeyCode.F) && canPlant && plantedSeed != null)
        {
            Debug.Log("Menyiram");
            WaterSeed();
        }
    }

    void PlantSeed()
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager != null)
        {
            if (inventoryManager.HasSeeds())
            {
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
                    GameObject seed = Instantiate(seedPrefab, plantPosition, Quaternion.identity);

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

                        inventoryManager.UseSeed();
                        Debug.Log("Benih berhasil ditanam. Jumlah benih tersisa: " + inventoryManager.GetSeedCount());
                    }
                }
                else
                {
                    Debug.LogWarning("Benih sudah ada di posisi ini.");
                }
            }
            else
            {
                Debug.LogWarning("Tidak ada benih untuk ditanam.");
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
