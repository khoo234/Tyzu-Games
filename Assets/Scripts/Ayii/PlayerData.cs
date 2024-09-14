using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] posisi;

    public PlayerData(PlayerStatus player)
    {
        posisi = new float[3];
        posisi[0] = player.transform.position.x;
        posisi[1] = player.transform.position.y;
        posisi[2] = player.transform.position.z;
    }
}
