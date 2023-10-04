using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
public float volume;
public AudioMixer mixer;

    public void playGame() {
       SceneManager.LoadSceneAsync("Level 1");
    }

    public void openSettings() {
       SceneManager.LoadSceneAsync("Settings");
    }

    public void quitGame() {
       SceneManager.LoadSceneAsync("MainMenu");
    }

    public void SetVolume(float volume) {
      mixer.SetFloat("Volume", volume);
    }

public void Navegacao(string nomeDaCena)
{
    SceneManager.LoadScene(nomeDaCena);
}

}
