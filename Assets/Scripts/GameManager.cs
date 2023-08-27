using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private Animator _gameOver;
    [SerializeField] private Animator _win;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    public void GameOver()
    {
        _gameOver.SetTrigger("SlideIn");
    }

    public void Win()
    {
        _win.SetTrigger("SlideIn");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
