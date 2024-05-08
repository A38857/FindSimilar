using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplayControl : MonoBehaviour
{
    private int times=1;
    public static int prevIndexscene;
    public AudioClip ReplaySound;
    private AudioSource ReplayAudio;
    public static int Score;
    public Text ScoreDisplay;


    private void Start()
    {
        ReplayAudio = GetComponent<AudioSource>();
        ReplayAudio.clip = ReplaySound;
        ReplayAudio.Play();
        ScoreDisplay.text = Score.ToString();

    }
    private void Update()
    {

    }
    public void NextLevel()
    {

        prevIndexscene++;
        Debug.Log("Times1: " + prevIndexscene);
        SceneManager.LoadScene(prevIndexscene);
        Debug.Log("Times2: " + prevIndexscene);
    }


    public void ReplaySQuit()
    {
        times = 1;
        Application.Quit();
    }

}
