using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryCanvas;
    public MonoBehaviour playerMovementScript;
    public TextMeshProUGUI seedCountText;
    private bool isInventoryOpen = false;
    private int seedCount = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isInventoryOpen)
        {
            OpenInventory();
        }
    }

    public void OpenInventory()
    {
        isInventoryOpen = true;
        inventoryCanvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
        playerMovementScript.enabled = false;
        UpdateSeedCountUI();  // Update UI saat inventory dibuka
    }

    public void CloseInventory()
    {
        isInventoryOpen = false;
        inventoryCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerMovementScript.enabled = true;
    }

    public void AddSeed(int amount)
    {
        seedCount += amount;
        UpdateSeedCountUI();
    }

    public bool HasSeeds()
    {
        return seedCount > 0;
    }

    public void UseSeed()
    {
        if (seedCount > 0)
        {
            seedCount--;
            UpdateSeedCountUI();
        }
    }

    public int GetSeedCount()
    {
        return seedCount;
    }

    private void UpdateSeedCountUI()
    {
        if (seedCountText != null)
        {
            seedCountText.text = "Benih: " + seedCount;
        }
    }
}
