using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenanamBenih : MonoBehaviour
{
    [Header ("Seed")]
    public GameObject seedPrefab;       // Prefab untuk benih biasa
    public GameObject seedRarePrefab;   // Prefab untuk benih rare

    [Header ("Efek")]
    public GameObject waterVFXPrefab;
    public GameObject fertilizerPrefab; // Prefab untuk pupuk
    public bool canPlant = false;

    [Header("Ladang Rare?")]
    public bool isSeedRareField = false; // Menandakan apakah ladang ini untuk benih rare

    public Benih plantedSeed;
    public bool hasPlantedSeed = false; // Flag untuk mengecek apakah sudah menanam benih
    private bool isFertilized = false; // Flag untuk mengecek apakah pupuk sudah digunakan
    private bool wateredd = false; // Flag untuk mengecek apakah pupuk sudah digunakan

    [Header("Eventnya")]
    public VisualDialog Dialog;

    public GameObject TekanE;
    public TMP_Text Textnya;
    public float searchRadius;

    [Header("Notif")]
    public Notifikasi TidakAda;
    public Notifikasi NeedFertilizer;

    public AudioSource Plant;
    public AudioSource Harvest;

    private string seedKey; // Key untuk menyimpan status seed

    void Start()
    {
        seedKey = "SeedData_" + transform.position.x + "_" + transform.position.z;

        LoadSeedData();
    }

    void Update()
    {
        if (plantedSeed == null)
        {
            if (Input.GetKeyDown(KeyCode.E) && canPlant && !hasPlantedSeed)
            {
                PlantSeed();
                Plant.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && canPlant && isSeedRareField && plantedSeed != null)
        {
            if (plantedSeed.CompareTag("SeedRare"))
            {
                Debug.Log("Memberi pupuk");
                UseFertilizer();
            }
        }

        if (!wateredd)
        {
            if (Input.GetKeyDown(KeyCode.F) && canPlant && plantedSeed != null)
            {
                if (isSeedRareField && !isFertilized)
                {
                    NeedFertilizer.ShowPopup();
                    return;
                }

                WaterSeed();
                wateredd = true;
            }
        }

        if (plantedSeed != null && isSeedRareField && !isFertilized)
        {
            Textnya.text = "Press Q to Use Fertilizer";
        }
        else if(plantedSeed != null && isSeedRareField && isFertilized){
            Textnya.text = "Press F to Give Water";
        }
        else if (plantedSeed != null && !isSeedRareField)
        {
            Textnya.text = "Press F to Give Water";
        }
        else
        {
            Textnya.text = "Press E to Plant";
        }

        if (plantedSeed != null && plantedSeed.isWatered)
        {
            Textnya.text = "";
        }

        if(plantedSeed != null && plantedSeed.Panen)
        {
            Textnya.text = "Press F to harvest";
        }

        if (plantedSeed != null)
        {
            if (Input.GetKeyDown(KeyCode.F) && plantedSeed.canBeDestroyed)
            {
                ClearSeedData();
                Harvest.Play();
                plantedSeed.DestroySeed();
                plantedSeed.AddBibitToInventory();
                wateredd = false;
            }
        }

        CariBenih();
        CariDialog();
    }

    void PlantSeed()
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager != null)
        {
            GameObject seedToPlant = null;

            // Pilih prefab benih yang sesuai dengan jenis ladang
            if (isSeedRareField && inventoryManager.HasSeedRare())
            {
                seedToPlant = seedRarePrefab;
                inventoryManager.UseSeedRare(); // Mengurangi jumlah benih rare dari inventory
            }
            else if (!isSeedRareField && inventoryManager.HasSeeds())
            {
                seedToPlant = seedPrefab;
                inventoryManager.UseSeed(); // Mengurangi jumlah benih biasa dari inventory
            }
            else 
            {
                TidakAda.ShowPopup();
                return;
            }

            Vector3 plantPosition = new Vector3(transform.position.x, 2.3f, transform.position.z);
            GameObject seed = Instantiate(seedToPlant, plantPosition, Quaternion.identity);

            Benih benihScript = seed.GetComponent<Benih>();
            if (benihScript != null)
            {
                benihScript.Ditanam = true;
                plantedSeed = benihScript;
                hasPlantedSeed = true;

                SaveSeedData();
            }
        }
    }

    void WaterSeed()
    {
        if (plantedSeed != null)
        {
            GameObject waterVFX = Instantiate(waterVFXPrefab, transform.position, Quaternion.identity);
            Destroy(waterVFX, 2f);

            plantedSeed.StartWatering();
            isFertilized = false; // Reset status pupuk setelah menyiram
        }
    }

    void UseFertilizer()
    {
        if (plantedSeed != null)
        {
            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            if (inventoryManager != null && inventoryManager.HasPupuk())
            {
                inventoryManager.UsePupuk(); // Mengurangi pupuk dari inventory
                isFertilized = true;
            }
            else
            {
                Debug.LogWarning("Pupuk tidak cukup.");
            }
        }
    }

    void SaveSeedData()
    {
        PlayerPrefs.SetInt(seedKey + "_planted", 1);
        PlayerPrefs.SetInt(seedKey + "_isRare", isSeedRareField ? 1 : 0);
        PlayerPrefs.Save();
    }

    void LoadSeedData()
    {
        if (PlayerPrefs.HasKey(seedKey + "_planted") && PlayerPrefs.GetInt(seedKey + "_planted") == 1)
        {
            bool isRare = PlayerPrefs.GetInt(seedKey + "_isRare") == 1;
            GameObject seedToSpawn = isRare ? seedRarePrefab : seedPrefab;

            Vector3 plantPosition = new Vector3(transform.position.x, 2.3f, transform.position.z);
            GameObject seed = Instantiate(seedToSpawn, plantPosition, Quaternion.identity);

            Benih benihScript = seed.GetComponent<Benih>();
            if (benihScript != null)
            {
                benihScript.Ditanam = true;
                plantedSeed = benihScript;
                hasPlantedSeed = true;
            }

            Debug.Log("Benih dimuat dari penyimpanan.");
        }
    }

    void ClearSeedData()
    {
        PlayerPrefs.DeleteKey(seedKey + "_planted");
        PlayerPrefs.DeleteKey(seedKey + "_isRare");
        PlayerPrefs.Save();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlant = true;
            TekanE.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlant = false;
            TekanE.SetActive(false);
        }
    }

    void CariBenih()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
        foreach (var collider in colliders)
        {
            Benih benihh = collider.GetComponent<Benih>();
            if (benihh != null)
            {
                plantedSeed = benihh;
                break;
            }
        }
    }
    void CariDialog()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5);
        foreach (var collider in colliders)
        {
            VisualDialog dialogg = collider.GetComponent<VisualDialog>();
            if (dialogg != null)
            {
                Dialog = dialogg;
            }
        }
    }
}
