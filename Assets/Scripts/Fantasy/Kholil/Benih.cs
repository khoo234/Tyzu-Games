using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Benih : MonoBehaviour
{
    [Header("Value")]
    public int benihValue = 1;

    [Header("Menampilkan Informasi Object")]
    public string NamaObject;
    public string NamaObject2;
    public TMP_Text Text;

    [Header("Tampilkan Waktu")]
    public TMP_Text WaktuText;
    public int waktuDurasi = 10;
    private float waktuBerjalan = 0f;
    private bool waktuAktif = false;

    [Header("Bentuk Benih")]
    public GameObject Biji;
    public GameObject SudahSelesai;

    [Header("Event")]
    public UnityEvent Setengah;

    public bool Ditanam = false;

    private void Start()
    {
        Text.text = NamaObject;

        WaktuText.gameObject.SetActive(Ditanam);
        if (Ditanam)
        {
            WaktuText.gameObject.SetActive(true);
            waktuBerjalan = 0f;
            waktuAktif = true;
            WaktuText.text = waktuDurasi.ToString();
        }
        else
        {
            waktuAktif = false;
        }
    }

    private void Update()
    {
        if (waktuAktif)
        {
            waktuBerjalan += Time.deltaTime;

            int waktuSisa = Mathf.CeilToInt(waktuDurasi - waktuBerjalan);

            WaktuText.text = waktuSisa.ToString();

            if (waktuSisa <= 0)
            {
                waktuAktif = false;
                WaktuText.gameObject.SetActive(false);
                Debug.Log("Waktu telah tercapai!");
                Biji.SetActive(false);
                SudahSelesai.SetActive(true);
                Text.text = NamaObject2;
            }
            if (waktuSisa == 5)
            {
                Setengah?.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Benih collected! Value: " + benihValue);

            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            if (inventoryManager != null)
            {
                inventoryManager.AddSeed(benihValue);
            }

            Destroy(gameObject);
        }
    }
}
