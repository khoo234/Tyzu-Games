using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    private Vector3 direction;
    public int baseDamage = 20; // Damage dasar peluru

    public void Initialize(Vector3 fireDirection)
    {
        direction = fireDirection.normalized;
    }

    private void Start()
    {
        if (direction == Vector3.zero)
        {
            direction = transform.forward;
        }

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStatus playerStatus = other.gameObject.GetComponent<PlayerStatus>();
            Skill2 skill2 = other.gameObject.GetComponent<Skill2>();

            if (playerStatus != null)
            {
                int damage = baseDamage;
                if (skill2 != null && skill2.IsArmorActive())
                {
                    // Cast armor value to int before subtraction
                    int armorValue = Mathf.RoundToInt(skill2.GetArmorValue());
                    damage -= armorValue; // Kurangi damage dengan nilai armor
                    damage = Mathf.Max(damage, 0); // Pastikan damage tidak negatif
                }

                playerStatus.KenaDamage(damage);

                // Tambahkan efek visual untuk menunjukkan armor aktif
                if (skill2 != null && skill2.IsArmorActive())
                {
                    // Misalnya, spawn efek visual di posisi peluru
                    // Anda bisa menambahkan efek visual sesuai kebutuhan
                    Debug.Log("Armor aktif - Damage dikurangi!");
                }
            }
        }
    }
}
