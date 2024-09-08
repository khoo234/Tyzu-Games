using StarterAssets;
using System.Collections;
using UnityEngine;

public class Skill2 : MonoBehaviour
{
    public Animator animator;
    private ThirdPersonController playerMovement;
    public GameObject vfxPrefab1;
    public Transform vfxSpawnPoint1;
    public float attackDelay = 0.5f;
    public float baseArmorDuration = 5f; // Base duration for armor
    public float armorDurationLevel2 = 10f; // Armor duration for level 2
    public float armorDurationLevel3 = 15f; // Armor duration for level 3
    public int baseArmorValue = 10; // Base value for armor
    public int armorValueLevel2 = 20; // Armor value for level 2
    public int armorValueLevel3 = 30; // Armor value for level 3
    private bool isArmorActive = false;
    private int skillLevel = 1; // Skill level (1 for initial, 2 for level 2, 3 for level 3)

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<ThirdPersonController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            playerMovement.enabled = false;
            animator.SetTrigger("Skill2");
            StartCoroutine(ResetMovementAfterAttack(attackDelay));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) // Press '2' to increase skill level
        {
            LevelUpSkill();
        }
    }

    public void SpawnSkill2VFX1()
    {
        if (vfxPrefab1 != null && vfxSpawnPoint1 != null)
        {
            GameObject vfxInstance1 = Instantiate(vfxPrefab1, vfxSpawnPoint1.position, vfxSpawnPoint1.rotation);
            vfxInstance1.transform.parent = transform;
            Destroy(vfxInstance1, GetVFXLifetime()); // Use armor duration for VFX lifetime

            // Display information or additional effects if armor is active
            if (isArmorActive)
            {
                Debug.Log("Armor aktif - VFX Spawned!");
                // Add visual effects or other logic when armor is active
            }
        }
        else
        {
            Debug.LogWarning("VFX Prefab 1 atau VFX Spawn Point 1 belum diatur!");
        }
    }

    public void ActivateArmor()
    {
        isArmorActive = true;
        StartCoroutine(DeactivateArmorAfterDuration());
    }

    IEnumerator DeactivateArmorAfterDuration()
    {
        yield return new WaitForSeconds(GetArmorDuration());
        isArmorActive = false;
    }

    IEnumerator ResetMovementAfterAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerMovement.enabled = true;
    }

    public bool IsArmorActive()
    {
        return isArmorActive;
    }

    public float GetArmorValue()
    {
        switch (skillLevel)
        {
            case 1:
                return baseArmorValue;
            case 2:
                return armorValueLevel2;
            case 3:
                return armorValueLevel3;
            default:
                return 0;
        }
    }

    public float GetArmorDuration()
    {
        switch (skillLevel)
        {
            case 1:
                return baseArmorDuration;
            case 2:
                return armorDurationLevel2;
            case 3:
                return armorDurationLevel3;
            default:
                return 0;
        }
    }

    private float GetVFXLifetime()
    {
        return GetArmorDuration(); // VFX lifetime matches armor duration
    }

    private void LevelUpSkill()
    {
        if (skillLevel < 3) // Level up to level 3
        {
            skillLevel++;
            Debug.Log($"Skill level increased to {skillLevel}.");
            Debug.Log($"New Armor Duration: {GetArmorDuration()} seconds.");
            Debug.Log($"New Armor Value: {GetArmorValue()}.");
        }
        else
        {
            Debug.Log("Skill is already at maximum level.");
        }
    }
}
