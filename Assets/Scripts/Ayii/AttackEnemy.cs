using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    [SerializeField] private int JumlahDamage = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStatus enemyHealth = collision.gameObject.GetComponent<PlayerStatus>();
            enemyHealth.KenaDamage(JumlahDamage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStatus enemyHealth = other.gameObject.GetComponent<PlayerStatus>();
            enemyHealth.KenaDamage(JumlahDamage);
        }
    }
}
