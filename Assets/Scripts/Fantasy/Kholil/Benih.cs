using UnityEngine;

public class Benih : MonoBehaviour
{
    public int benihValue = 1; // Nilai benih yang ditambahkan

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Tampilkan nilai benih di konsol
            Debug.Log("Benih collected! Value: " + benihValue);

            // Update inventory manager dengan jumlah benih yang diambil
            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            if (inventoryManager != null)
            {
                inventoryManager.AddSeed(benihValue);
            }

            // Hancurkan objek benih
            Destroy(gameObject);
        }
    }
}
