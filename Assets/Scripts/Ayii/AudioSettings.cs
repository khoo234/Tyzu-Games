using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Audio Tag")]
    public string musicTag = "Music";
    public string sfxTag = "SFX";

    [Header("Audio Slider")]
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("All Audio")]
    public List<AudioSource> musicSources = new List<AudioSource>();
    public List<AudioSource> sfxSources = new List<AudioSource>();

    [Header("Audio Information")]
    public GameObject MusicFull;
    public GameObject MusicMute;
    public GameObject SFXFull;
    public GameObject SFXMute;

    private bool isMusicMuted;
    private bool isSFXMuted;

    private float defaultMusicVolume = 1f;
    private float defaultSfxVolume = 1f;
    private float lastMusicVolume;
    private float lastSFXVolume;

    void Start()
    {
        lastMusicVolume = PlayerPrefs.GetFloat("LastMusicVolume", defaultMusicVolume);
        lastSFXVolume = PlayerPrefs.GetFloat("LastSFXVolume", defaultSfxVolume);

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", lastMusicVolume);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", lastSFXVolume);

        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);

        isMusicMuted = PlayerPrefs.GetInt("IsMusicMuted", 0) == 1;
        isSFXMuted = PlayerPrefs.GetInt("IsSFXMuted", 0) == 1;

        if (isMusicMuted) MuteMusic();
        if (isSFXMuted) MuteSFX();
    }

    void Update()
    {
        musicSources = FindAudioSourcesByTag(musicTag);
        sfxSources = FindAudioSourcesByTag(sfxTag);

        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);

        if (musicSlider.value > 0)
        {
            isMusicMuted = false;
        }
        else
        {
            isMusicMuted = true;
        }

        if (sfxSlider.value > 0)
        {
            isSFXMuted = false;
        }
        else
        {
            isSFXMuted = true;
        }

        UpdateMuteIcons();
    }

    public void OnMusicSliderChanged()
    {
        SetMusicVolume(musicSlider.value);
        lastMusicVolume = musicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("LastMusicVolume", lastMusicVolume);
        PlayerPrefs.Save();
    }

    public void OnSFXSliderChanged()
    {
        SetSFXVolume(sfxSlider.value);
        lastSFXVolume = sfxSlider.value;
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetFloat("LastSFXVolume", lastSFXVolume);
        PlayerPrefs.Save();
    }

    public void MuteMusic()
    {
        if (isMusicMuted)
        {
            lastMusicVolume = musicSlider.value;
            isMusicMuted = false;
            PlayerPrefs.SetInt("IsMusicMuted", 0);
        }
        else
        {
            musicSlider.value = lastMusicVolume;
            musicSlider.value = 0;
            isMusicMuted = true;
            PlayerPrefs.SetInt("IsMusicMuted", 1);
        }

        PlayerPrefs.Save();
    }

    public void MuteSFX()
    {
        if (isSFXMuted)
        {
            sfxSlider.value = lastSFXVolume;
            isSFXMuted = false;
            PlayerPrefs.SetInt("IsSFXMuted", 0);
        }
        else
        {
            lastSFXVolume = sfxSlider.value;
            sfxSlider.value = 0;
            isSFXMuted = true;
            PlayerPrefs.SetInt("IsSFXMuted", 1);
        }

        PlayerPrefs.Save();
    }

    private void SetMusicVolume(float volume)
    {
        foreach (AudioSource source in musicSources)
        {
            if (source != null)
            {
                source.volume = volume;
            }
        }
    }

    private void SetSFXVolume(float volume)
    {
        foreach (AudioSource source in sfxSources)
        {
            if (source != null)
            {
                source.volume = volume;
            }
        }
    }

    private List<AudioSource> FindAudioSourcesByTag(string tag)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        List<AudioSource> audioSources = new List<AudioSource>();

        foreach (GameObject obj in taggedObjects)
        {
            AudioSource audioSource = obj.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSources.Add(audioSource);
            }
        }
        return audioSources;
    }

    private void UpdateMuteIcons()
    {
        if (musicSlider.value > 0)
        {
            MusicFull.SetActive(true);
            MusicMute.SetActive(false);
        }
        else
        {
            MusicFull.SetActive(false);
            MusicMute.SetActive(true);
        }

        if (sfxSlider.value > 0)
        {
            SFXFull.SetActive(true);
            SFXMute.SetActive(false);
        }
        else
        {
            SFXFull.SetActive(false);
            SFXMute.SetActive(true);
        }
    }
}
