using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public AudioClip buttonClickSound;
    public AudioSource sfxSource;

    public void Play()
    {
        Time.timeScale = 1f;
        sfxSource.PlayOneShot(buttonClickSound);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameePlay");
    }
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }
    public void Quite()
    {

        Application.Quit();
        sfxSource.PlayOneShot(buttonClickSound);
    }
   /* public void PlayClick()
    {
        PlaySound(buttonClickSound);
    }*/


}

