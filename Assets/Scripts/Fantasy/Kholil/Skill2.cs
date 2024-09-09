using StarterAssets;
using System.Collections;
using UnityEngine;

public class Skill2 : MonoBehaviour
{
    [Header("Animator")]
    public Animator animator;
    private ThirdPersonController playerMovement;

    [Header("VFX")]
    public GameObject vfxPrefab1;
    public Transform vfxSpawnPoint1;

    [Header("Menyerang Delay")]
    public float attackDelay = 0.5f;

    [Header("Durasi Armor")]
    public float baseArmorDuration = 5f; // Base duration for armor
    public int baseArmorValue = 10; // Base value for armor

    [Header("Armor Level 2")]
    public float armorDurationLevel2 = 10f; // Armor duration for level 2
    public int armorValueLevel2 = 20; // Armor value for level 2

    [Header("Armor Level 3")]
    public float armorDurationLevel3 = 15f; // Armor duration for level 3
    public int armorValueLevel3 = 30; // Armor value for level 3

    private bool isArmorActive = false;
    public int skillLevel = 1; // Skill level (1 for initial, 2 for level 2, 3 for level 3)

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<ThirdPersonController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            playerMovement.enabled = false;
            animator.SetTrigger("Skill2");
            StartCoroutine(ResetMovementAfterAttack(attackDelay));
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

    // Method to set skill level from another script
    public void SetSkillLevel(int level)
    {
        if (level >= 1 && level <= 3)
        {
            skillLevel = level;
            Debug.Log($"Skill level set to {skillLevel}.");
            Debug.Log($"New Armor Duration: {GetArmorDuration()} seconds.");
            Debug.Log($"New Armor Value: {GetArmorValue()}.");
        }
        else
        {
            Debug.LogError("Invalid skill level.");
        }
    }
}
