using UnityEngine;

public class AIMeleeAttack : MonoBehaviour
{
    public string playerTag;
    private Transform player;
    public float speed = 3f;
    public float stoppingDistance = 1f;
    public float attackRange = 1.5f;
    public float raycastLength = 2f;
    public float avoidanceDistance = 1f;
    public LayerMask obstacleLayer;

    public Animator anim;
    private bool isMoving = false;

    private bool isStunned = false;
    private float stunEndTime;

    public GameObject meleeEffectPrefab;
    public Transform effectSpawnPoint;

    private void Start()
    {
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
                return;
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

                if (distance <= attackRange && !IsInAttackAnimation())
                {
                    anim.SetTrigger("Attack");
                }
            }
        }
    }

    public void Stun(float duration)
    {
        isStunned = true;
        stunEndTime = Time.time + duration;
        anim.SetBool("Walk", false);
    }

    private void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player with tag " + playerTag + " not found!");
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

    private void SpawnMeleeEffect()
    {
        if (meleeEffectPrefab != null && effectSpawnPoint != null)
        {
            Instantiate(meleeEffectPrefab, effectSpawnPoint.position, effectSpawnPoint.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && IsInAttackAnimation())
        {
            Debug.Log("Dealing melee damage to player!");
        }
    }

    public void OnAnimationEventMelee()
    {
        SpawnMeleeEffect();
    }
}
