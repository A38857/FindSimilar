using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroControl : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip StartSound;
    private AudioSource StartAudio;
    void Start()
    {
        StartAudio = GetComponent<AudioSource>();
        StartAudio.clip = StartSound;
        StartAudio.Play();
    }
  
    public void StartGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
