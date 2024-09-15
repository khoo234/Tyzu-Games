using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public Material materialKucing1; // Material untuk kucing pertama untuk badan 007
    public Material materialKucing2; // Material untuk kucing kedua untuk badan 007
    public Material materialBadan002_1; // Material pertama untuk badan 002
    public Material materialBadan002_2; // Material kedua untuk badan 002
    public string badanPartName007 = "badan 007"; // Nama bagian yang ingin diubah materialnya untuk badan 007
    public string badanPartName002 = "badan 002"; // Nama bagian yang ingin diubah materialnya untuk badan 002
    public GameObject switchEffectObject;

    private Renderer[] renderers; // Daftar Renderer dari semua bagian
    public bool isKoinExchanged = false; // Flag untuk memeriksa apakah koin sudah ditukar

    private void Start()
    {
        // Mendapatkan semua komponen Renderer dari bagian-bagian anak
        renderers = GetComponentsInChildren<Renderer>();

        // Pastikan salah satu material aktif pada awalnya
        SwitchToMaterialKucing1(badanPartName007);
    }

    private void Update()
    {
        // Jika tombol "1" ditekan, lakukan switch ke material Kucing 1 pada badan 007
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchEffectObject.SetActive(false);
            SwitchToMaterialBadan002_1();
            SwitchToMaterialKucing1(badanPartName007);
        }

        // Jika tombol "2" ditekan dan koin sudah ditukar, lakukan switch ke material Kucing 2 pada badan 007
        if (Input.GetKeyDown(KeyCode.Alpha2) && isKoinExchanged)
        {
            switchEffectObject.SetActive(true);
            SwitchToMaterialKucing2(badanPartName007);
            SwitchToMaterialBadan002_2();
        }
    }

    public void SwitchToMaterialKucing1(string partName)
    {
        SwitchMaterial(materialKucing1, partName);
    }

    public void SwitchToMaterialKucing2(string partName)
    {
        SwitchMaterial(materialKucing2, partName);
    }

    public void SwitchToMaterialBadan002_1()
    {
        SwitchMaterial(materialBadan002_1, badanPartName002);
    }

    public void SwitchToMaterialBadan002_2()
    {
        SwitchMaterial(materialBadan002_2, badanPartName002);
    }

    private void SwitchMaterial(Material newMaterial, string partName)
    {
        foreach (var renderer in renderers)
        {
            // Periksa jika nama bagian sama dengan partName
            if (renderer.gameObject.name == partName)
            {
                // Mengganti material dari bagian yang sesuai
                renderer.material = newMaterial;
            }
        }
    }
}
