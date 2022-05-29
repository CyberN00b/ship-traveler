using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _PauseMenu;
    [SerializeField]
    private GameObject _gameOver;
    [SerializeField]
    private GameObject _completedLevel;
    private bool _isPaused = false;
    private GameObject player;

    private void Start()
    {
        _PauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(_isPaused)
            {
                Resume();
            }
            else
            {
                if (_gameOver.activeSelf == false && _completedLevel.activeSelf == false)
                { Pause(); }
            }
        }
    }
    public void Resume()
    {
        _isPaused = false;
        _PauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        AudioSource audio = player.GetComponent<AudioSource>();
        audio.UnPause();
    }
    public void Pause()
    {
        _isPaused = true;
        _PauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
        AudioSource audio = player.GetComponent<AudioSource>();
        audio.Pause();
    }
    public void Restart() 
    { 
        SceneManager.LoadScene("Game"); 
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
