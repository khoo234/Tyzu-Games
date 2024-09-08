using UnityEngine;

public class Stun : MonoBehaviour
{
    public GameObject stunVFX; // VFX yang akan dimainkan saat musuh terkena jurus
    public float stunDuration = 2f; // Durasi stun dalam detik

    private void OnTriggerEnter(Collider other)
    {
        // Periksa jika objek yang terkena adalah musuh
        if (other.CompareTag("Enemy"))
        {
            // Mainkan VFX
            if (stunVFX != null)
            {
                Instantiate(stunVFX, other.transform.position, Quaternion.identity);
            }

            // Cegah musuh bergerak
            AIFollowPlayer enemyAI = other.GetComponent<AIFollowPlayer>();
            if (enemyAI != null)
            {
                enemyAI.Stun(stunDuration);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Periksa jika objek yang terkena adalah musuh
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Mainkan VFX
            if (stunVFX != null)
            {
                Instantiate(stunVFX, collision.transform.position, Quaternion.identity);
            }

            // Cegah musuh bergerak
            AIFollowPlayer enemyAI = collision.gameObject.GetComponent<AIFollowPlayer>();
            if (enemyAI != null)
            {
                enemyAI.Stun(stunDuration);
            }
        }
    }
}
