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

    }

    public void SetVolume(float volume)
    {
        audioMusicaDeFundo.volume = volume;
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