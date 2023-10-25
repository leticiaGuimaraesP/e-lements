using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioMusicaDeFundo;
    public AudioClip musicaDeFundo;


    void Start()
    {
        audioMusicaDeFundo.clip = musicaDeFundo;
        audioMusicaDeFundo.loop = true;
        audioMusicaDeFundo.Play();
        if (!PlayerPrefs.HasKey("audio-volume"))
        {
            PlayerPrefs.SetFloat("audio-volume", 5);
            Load();
        }
        else
        {
            Load();
        }
        volumeSlider.value = PlayerPrefs.GetFloat("audio-volume");

    }

    public void SetVolume(float volume)
    {
        audioMusicaDeFundo.volume = volume;
        Save();
    }

    private void Load()
    {
        audioMusicaDeFundo.volume = PlayerPrefs.GetFloat("audio-volume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("audio-volume", audioMusicaDeFundo.volume);
    }

    public void LigarSom()
    {
        audioMusicaDeFundo.enabled = true;
    }

    public void DesligarSom()
    {
        audioMusicaDeFundo.enabled = false;
    }

}