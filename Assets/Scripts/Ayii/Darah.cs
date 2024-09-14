using UnityEngine;

public class Darah : MonoBehaviour
{
    [Header("Darah")]
    [SerializeField] private int DarahSekarang;
    [SerializeField] private int MaxDarah;
    public AnimasiPosisi Notif;
    public Animator anim;
    public AIMeleeAttack Attack;
    public int CoinMiaw;
    private Benih benih;

    public HealthBar Script;
    private InventoryManager inventoryManager;
    public bool isDead = false;

    private void Awake()
    {
        MaxDarah = DarahSekarang;
    }

    private void Start()
    {
        Script = FindObjectOfType<HealthBar>();
        benih = FindAnyObjectByType<Benih>();
        Script.SetHealth(DarahSekarang, MaxDarah);
        Notif = FindAnyObjectByType<AnimasiPosisi>();
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
        inventoryManager.AddKoinFantasy(CoinMiaw);
        inventoryManager.AddMonster(1);
        Notif.ShowUIWithDelay();
    }
    public void SetBenih(Benih newBenih)
    {
        benih = newBenih;
    }
}
