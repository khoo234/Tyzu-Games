using UnityEngine;

public class Darah : MonoBehaviour
{
    [Header("Darah")]
    [SerializeField] private int DarahSekarang;
    [SerializeField] private int MaxDarah;
    public Animator anim;

    public HealthBar Script;
    private InventoryManager inventoryManager;
    private Benih benih;
    public bool isDead = false;

    private void Awake()
    {
        MaxDarah = DarahSekarang;
    }

    private void Start()
    {
        Script = FindObjectOfType<HealthBar>();

        Script.SetHealth(DarahSekarang, MaxDarah);
        benih = FindAnyObjectByType<Benih>();

        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public void KenaDamage(int Jumlah)
    {
        if (isDead) return;

        DarahSekarang -= Jumlah;
        Script.SetHealth(DarahSekarang, MaxDarah);
        if (DarahSekarang <= 0)
        {
            DarahSekarang = 0;
            isDead = true;
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

        inventoryManager.AddMonster(1);
    }

    public void SetBenih(Benih newBenih)
    {
        benih = newBenih;
    }
}
