using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource _blip;

    public void Play()
    {
        PlayBlip();
        SceneManager.LoadScene(1);
    }

    public void DisplayCredits()
    {
        PlayBlip();
    }

    public void Exit()
    {
        PlayBlip();
        Application.Quit();
    }

    private void PlayBlip()
    {
        _blip.Play();
    }
}
