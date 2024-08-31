using UnityEngine;

public class MenanamBenih : MonoBehaviour
{
    public GameObject seedPrefab;
    public bool canPlant = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPlant)
        {
            PlantSeed();
        }
    }

    void PlantSeed()
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager != null)
        {
            Debug.Log("InventoryManager ditemukan");

            if (inventoryManager.HasSeeds())
            {
                Vector3 plantPosition = transform.position + Vector3.up;
                GameObject seed = Instantiate(seedPrefab, plantPosition, Quaternion.identity);

                Benih benihScript = seed.GetComponent<Benih>();
                if (benihScript != null)
                {
                    // Mengatur Ditanam menjadi true
                    benihScript.Ditanam = true;
                }

                // Menonaktifkan collider benih agar tidak bisa diambil lagi
                Collider seedCollider = seed.GetComponent<Collider>();
                if (seedCollider != null)
                {
                    seedCollider.enabled = false;
                }
                
                // Kurangi benih dari inventory
                inventoryManager.UseSeed();
                Debug.Log("Benih berhasil ditanam. Jumlah benih tersisa: " + inventoryManager.GetSeedCount());
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlant = true; // Player memasuki area tanam
            Debug.Log("Player berada di area tanam.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlant = false; // Player keluar dari area tanam
            Debug.Log("Player keluar dari area tanam.");
        }
    }
}
