using UnityEngine;
using System.Collections.Generic;

public class MenyiramBenih : MonoBehaviour
{
    private bool canWater = false;

    // List to keep track of seeds that are watered
    private List<GameObject> wateredSeeds = new List<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canWater)
        {
            WaterSeed();
        }
    }

    void WaterSeed()
    {
        // Find all colliders within a small radius around the watering area
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);
        bool seedFound = false;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Seed")) // Directly check if the tag is "Seed"
            {
                seedFound = true;

                // Add the seed to the list of watered seeds
                if (!wateredSeeds.Contains(collider.gameObject))
                {
                    wateredSeeds.Add(collider.gameObject);
                }

                Debug.Log("Benih disiram. Menunggu 3 detik...");
                Invoke("FinishWatering", 3f);
                break; // Exit loop once a seed is found and watering is initiated
            }
        }

        if (!seedFound)
        {
            Debug.LogWarning("Tidak ada benih untuk disiram di area ini.");
        }
    }

    void FinishWatering()
    {
        Debug.Log("Penyiraman selesai setelah 3 detik.");
        // Destroy all watered seeds
        foreach (GameObject seed in wateredSeeds)
        {
            if (seed != null)
            {
                Destroy(seed);
            }
        }
        // Clear the list after destroying the seeds
        wateredSeeds.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canWater = true;
            Debug.Log("Player siap menyiram.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canWater = false;
            Debug.Log("Player keluar dari area penyiraman.");
        }
    }
}
