using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // Nilai koin yang ditambahkan

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Tambahkan koin ke total koin
            GameManager.Instance.AddCoins(coinValue);
            // Hancurkan koin
            Destroy(gameObject);
        }
    }
}
