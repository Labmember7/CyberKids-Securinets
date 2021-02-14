using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider sfxSlider;
    public Slider musicSlider;
    public bool deleteAllPlayerPrefs = false;
    void Awake()
    {
        if (deleteAllPlayerPrefs)
        {
             PlayerPrefs.DeleteAll();
        }
    }
    public void Start()
    {
        GetVolumePrefs();
    }
    public void GetVolumePrefs()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume" ,0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume",0.5f);

    }
    public void UpdateSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("SFXVolume", volume);

    }
    public void UpdateMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);

    }

    // public AudioSource audioSource;
    /*void Update()
    {
        Debug.Log(GameObject.FindObjectsOfType<Button>().Length);

        foreach (Button b in GameObject.FindObjectsOfType<Button>())
        {
            b.onClick.AddListener(() => audioSource.Play());
        }
    }*/
}
