using UnityEngine;

public class Darah : MonoBehaviour
{
    [Header("Darah")]
    [SerializeField] private int DarahSekarang;
    [SerializeField] private int MaxDarah;
    public Animator anim;

    public HealthBar Script;
    public InventoryManager inventoryManager; // Referensi ke InventoryManager
    private Benih benih; // Referensi ke Benih
    public bool isDead = false; // Variabel untuk memeriksa status kematian

    private void Awake()
    {
        MaxDarah = DarahSekarang;
    }

    private void Start()
    {
        if (Script == null)
            Script = FindObjectOfType<HealthBar>();

        if (Script != null)
            Script.SetHealth(DarahSekarang, MaxDarah);
        benih = FindAnyObjectByType<Benih>();

        // Menemukan InventoryManager
        inventoryManager = FindObjectOfType<InventoryManager>(); // Pastikan InventoryManager ada di scene
    }

    public void KenaDamage(int Jumlah)
    {
        if (isDead) return; // Cek apakah karakter sudah mati

        Debug.Log("Damage Received: " + Jumlah); // Debug untuk memastikan damage yang diterima
        DarahSekarang -= Jumlah;
        Script.SetHealth(DarahSekarang, MaxDarah);
        if (DarahSekarang <= 0)
        {
            DarahSekarang = 0;
            isDead = true; // Tandai karakter sebagai mati
            HandleDeath();
        }
        else
        {
            anim.SetTrigger("Hit");
        }
    }

    private void HandleDeath()
    {
        anim.SetBool("Die", true);
        Destroy(gameObject, 2f);

        // Menambahkan monster ke inventori
        if (inventoryManager != null)
        {
            inventoryManager.AddMonster(1);  // Tambahkan 1 monster ke inventory
        }

        // Implementasikan logika tambahan untuk kematian, seperti drop item, dll.
    }

    // Add the SetBenih method
    public void SetBenih(Benih newBenih)
    {
        benih = newBenih;
        // Implement additional logic if needed
    }
}
