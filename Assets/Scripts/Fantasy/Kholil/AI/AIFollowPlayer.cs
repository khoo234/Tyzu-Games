using UnityEngine;

public class AIFollowPlayer : MonoBehaviour
{
    public string playerTag = "playerattack"; // Tag to identify the player
    public float speed = 3f;
    public float stoppingDistance = 1f;
    public float raycastLength = 2f;
    public float avoidanceDistance = 1f;
    public LayerMask obstacleLayer;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float initialFireRate = 5f;
    public float subsequentFireRate = 1f;

    private float nextFireTime;
    public Animator anim;
    private bool isMoving = false;

    private bool isStunned = false;
    private float stunEndTime;
    private Transform player;

    private void Start()
    {
        nextFireTime = Time.time + initialFireRate;
        FindPlayer();
    }

    private void Update()
    {
        if (isStunned)
        {
            if (Time.time >= stunEndTime)
            {
                isStunned = false;
            }
            else
            {
                return; // Exit the Update method while stunned
            }
        }

        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > stoppingDistance)
            {
                Vector3 direction = new Vector3(player.position.x - transform.position.x, 0, player.position.z - transform.position.z).normalized;

                RaycastHit hit;
                if (!Physics.Raycast(transform.position, direction, out hit, raycastLength, obstacleLayer))
                {
                    MoveTowardsPlayer(direction);
                }
                else
                {
                    Vector3 avoidanceDirection = GetAvoidanceDirection(direction);
                    MoveTowardsPlayer(avoidanceDirection);
                }

                RotateTowardsPlayer();
            }
            else
            {
                anim.SetBool("Walk", false);
                isMoving = false;

                RotateTowardsPlayer();
            }

            if (Time.time >= nextFireTime && !isMoving && !IsInAttackAnimation())
            {
                anim.SetTrigger("Attack");
                nextFireTime = Time.time + initialFireRate;
            }
        }
    }

    public void Stun(float duration)
    {
        isStunned = true;
        stunEndTime = Time.time + duration;
        anim.SetBool("Walk", false); // Stop the walking animation while stunned
    }

    private void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void MoveTowardsPlayer(Vector3 direction)
    {
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
        Collider[] hitColliders = Physics.OverlapSphere(newPosition, avoidanceDistance, obstacleLayer);

        if (hitColliders.Length == 0)
        {
            transform.position = newPosition;
            anim.SetBool("Walk", true);
            isMoving = true;
        }
    }

    private Vector3 GetAvoidanceDirection(Vector3 direction)
    {
        Vector3 right = Vector3.Cross(Vector3.up, direction).normalized;
        Vector3 left = -right;

        if (!Physics.Raycast(transform.position, right, avoidanceDistance, obstacleLayer))
        {
            return right;
        }
        else if (!Physics.Raycast(transform.position, left, avoidanceDistance, obstacleLayer))
        {
            return left;
        }
        else
        {
            return -direction;
        }
    }

    private void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Spawn the bullet at the correct time during the attack animation
            Invoke("SpawnBullet", 0.6f);
        }
    }

    private void SpawnBullet()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Calculate the fire direction
            Vector3 fireDirection = (player.position - firePoint.position).normalized;

            // Rotate the firePoint to face the player
            firePoint.rotation = Quaternion.LookRotation(fireDirection);

            // Instantiate the bullet
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();

            if (bulletScript != null)
            {
                bulletScript.Initialize(fireDirection);
            }
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = new Vector3(player.position.x - transform.position.x, 0, player.position.z - transform.position.z).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

        Quaternion currentRotation = transform.rotation;
        Quaternion newRotation = Quaternion.Euler(currentRotation.eulerAngles.x, targetRotation.eulerAngles.y, currentRotation.eulerAngles.z);

        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * speed);
    }

    private bool IsInAttackAnimation()
    {
        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
        return currentState.IsName("Attack");
    }

    private bool IsInTransition()
    {
        return anim.IsInTransition(0);
    }

    public void OnAnimationEventShoot()
    {
        SpawnBullet();
    }

    private void OnAnimatorMove()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            // Delay shooting the bullet until the animation is halfway
            Shoot();
        }
    }
}
