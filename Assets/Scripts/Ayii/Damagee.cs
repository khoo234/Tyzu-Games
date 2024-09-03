using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagee : MonoBehaviour
{
    [SerializeField] private int JumlahDamage = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Darah enemyHealth = collision.gameObject.GetComponent<Darah>();
            Debug.Log("Ada Enemy!");
            if (enemyHealth != null)
            {
                enemyHealth.KenaDamage(JumlahDamage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Darah enemyHealth = other.gameObject.GetComponent<Darah>();

            if (enemyHealth != null)
            {
                enemyHealth.KenaDamage(JumlahDamage);
            }
        }
    }
}
