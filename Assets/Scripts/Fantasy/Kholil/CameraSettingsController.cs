using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CameraSettingsController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;  // Referensi ke FreeLook camera
    public Slider xAxisSpeedSlider;  // Slider untuk mengatur kecepatan X axis
    public Slider yAxisSpeedSlider;  // Slider untuk mengatur kecepatan Y axis
    public Slider bottomRigRadiusSlider;  // Slider untuk mengatur Radius Bottom Rig

    // Fungsi yang dipanggil saat awal permainan
    void Start()
    {
        // Setel slider ke nilai maksimum saat awal permainan
        xAxisSpeedSlider.value = xAxisSpeedSlider.maxValue;
        yAxisSpeedSlider.value = yAxisSpeedSlider.maxValue;
        bottomRigRadiusSlider.value = bottomRigRadiusSlider.maxValue;

        // Terapkan pengaturan awal
        ApplySettings();
    }

    // Fungsi untuk menerapkan perubahan saat tombol Apply ditekan
    public void ApplySettings()
    {
        // Membatasi nilai X Axis speed antara 0 dan 300
        float xAxisSpeed = Mathf.Clamp(xAxisSpeedSlider.value, 0, 300);
        freeLookCamera.m_XAxis.m_MaxSpeed = xAxisSpeed;

        // Membatasi nilai Y Axis speed antara 0 dan 2
        float yAxisSpeed = Mathf.Clamp(yAxisSpeedSlider.value, 0, 2);
        freeLookCamera.m_YAxis.m_MaxSpeed = yAxisSpeed;

        // Membatasi nilai Radius Bottom Rig, misalnya antara 0 dan 10
        float bottomRigRadius = Mathf.Clamp(bottomRigRadiusSlider.value, 0, 10);
        freeLookCamera.m_Orbits[2].m_Radius = bottomRigRadius; // m_Orbits[2] untuk Bottom Rig

    }
}
