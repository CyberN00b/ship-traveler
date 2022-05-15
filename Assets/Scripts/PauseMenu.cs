using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _PauseMenu;
    private bool _isPaused = false;

    private void Start()
    {
        _PauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
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
                Pause();
            }
        }
    }
    public void Resume()
    {
        _isPaused = false;
        _PauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        _isPaused = true;
        _PauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
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
