using UnityEngine;
using System.Collections.Generic;

public class MenanamBenih : MonoBehaviour
{
    public GameObject seedPrefab;
    private bool canPlant = false;
    private bool isPlanted = false;
    private List<GameObject> seedsInField = new List<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPlant && !isPlanted)
        {
            PlantSeed();
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
                GameObject seed = Instantiate(seedPrefab, plantPosition, Quaternion.identity);
                seedsInField.Add(seed);

                isPlanted = true;
                Collider seedCollider = seed.GetComponent<Collider>();
                if (seedCollider != null)
                {
                    seedCollider.enabled = false;
                }

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
            canPlant = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlant = false;
        }
    }

    public List<GameObject> GetSeedsInField()
    {
        return seedsInField;
    }
}
