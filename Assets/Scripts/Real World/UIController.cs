using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject firstPersonUI; // Referensi ke Canvas atau elemen UI untuk mode first-person

    void Start()
    {
        // Sembunyikan UI pada awalnya
        if (firstPersonUI != null)
        {
            firstPersonUI.SetActive(false);
        }
    }

    public void ShowFirstPersonUI()
    {
        if (firstPersonUI != null)
        {
            firstPersonUI.SetActive(true);
        }
    }

    public void HideFirstPersonUI()
    {
        if (firstPersonUI != null)
        {
            firstPersonUI.SetActive(false);
        }
    }
}
