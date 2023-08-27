using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource _blip;
    [SerializeField] private Animator _animator;

    public void Play()
    {
        PlayBlip();
        SceneManager.LoadScene(1);
    }

    public void DisplayCredits()
    {
        _animator.SetTrigger("SlideIn");
        PlayBlip();
    }

    public void CloseCredits()
    {
        _animator.SetTrigger("SlideOut");
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
