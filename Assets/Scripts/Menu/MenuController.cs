using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private GameObject current_menu = null;
    private int strength = 0;
    private bool is_time_stoped = false;
    void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }
    public void EnableMenu(GameObject menu, int menu_strength, bool is_stop = true) 
    {
        if (menu_strength > strength) 
        {
            if ( current_menu != null)
                current_menu.SetActive(false);
            menu.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            current_menu = menu;
            if (is_stop) 
                Time.timeScale = 0f;
            else 
                Time.timeScale = 1f;
            is_time_stoped = is_stop;
            strength = menu_strength;
        }
    }
    public void DisableMenu() 
    {
        if (is_time_stoped) 
        {
            Time.timeScale = 1f;
            is_time_stoped = false;
        }
        strength = 0;
        current_menu.SetActive(false);
        current_menu = null;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DisableMenu(string name) 
    {
        if (current_menu.name == name)
            DisableMenu();
    }
    public bool IsEnabled(string name) 
    {
        return current_menu != null && current_menu.name == name;
    }
    public bool IsEnabled() 
    {
        return current_menu != null;
    }
    public void ToMainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 1f;
        is_time_stoped = false;
        current_menu = null;
        strength = 0;
    }
    public void StartGame() 
    {
        SceneManager.LoadScene("Game"); 
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        is_time_stoped = false;
        current_menu = null;
        strength = 0;
    }
}
