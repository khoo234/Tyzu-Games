using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IsiNama : MonoBehaviour
{
    [SerializeField] private NamaPlayer namaPlayer;
    public TMP_InputField InputNama;
    public void SetPlayerName()
    {
        string playerName = InputNama.text.Trim();
        if (!string.IsNullOrEmpty(playerName))
        {
            namaPlayer.Nama = playerName;
        }
    }

}
