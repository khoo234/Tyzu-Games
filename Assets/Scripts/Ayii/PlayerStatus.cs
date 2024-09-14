using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;

public class PlayerStatus : MonoBehaviour
{
    [Header("Nama")]
    public string NamaPlayer;

    [Header("Darah")]
    [SerializeField] private int DarahSekarang;
    [SerializeField] private int MaxDarah;

    [Header("Animator")]
    public Animator anim;

    [Header("Info")]
    public TMP_Text Min;
    public TMP_Text Max;

    [Header("Script")]
    public HealthBar Script;

    [Header("Notif")]
    public TMP_Text Angka;
    public AnimasiPosisi Notif;

    [Header("Posisi")]
    public Vector3 defaultPosition;
    private Benih Seed;

    [Header("Search Settings")]
    public float searchRadius = 2f;

    private void Awake()
    {
        MaxDarah = DarahSekarang;
    }

    void Update()
    {
        Min.text = DarahSekarang.ToString();
        Max.text = MaxDarah.ToString();
        if (DarahSekarang <= 0)
        {
            Mati();
        }

        if (DarahSekarang > MaxDarah)
        {
            DarahSekarang = 100;
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
        foreach (var collider in colliders)
        {
            Benih benih = collider.GetComponent<Benih>();
            if (benih != null)
            {
                Seed = benih;
                break; // Hentikan pencarian jika sudah menemukan Benih
            }
        }
    }

    public void KenaDamage(int Jumlah)
    {
        DarahSekarang -= Jumlah;
        anim.SetTrigger("Hit");

        Script.SetHealth(DarahSekarang, MaxDarah);
    }

    public void Mati()
    {
        anim.SetBool("Death", true);
        Debug.Log("Kamu Mati!");
    }

    public void Heal(int Heall)
    {
        DarahSekarang += Heall;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Seed != null && !Seed.Ditanam)
        {
            if (other.CompareTag("SeedRare"))
            {
                Notif.ShowUIWithDelay();
                Angka.text = "1 Rare Seed";
            }
            else if (other.CompareTag("Seed"))
            {
                Notif.ShowUIWithDelay();
                Angka.text = "1 Basic Seed";
            }
        }
    }

    public void SavePlayer()
    {
        SistemSave.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SistemSave.LoadPlayer();

        if (data != null)
        {
            Vector3 position;
            position.x = data.posisi[0];
            position.y = data.posisi[1];
            position.z = data.posisi[2];
            transform.position = position;
        }
    }

    public void ResetPosisi()
    {
        // Menghapus file save player
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("File save telah dihapus.");
        }

        transform.position = defaultPosition;
    }
}
