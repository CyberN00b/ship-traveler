using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CompletedLevel : MonoBehaviour
{
    Button button_continue = null;
    Button button_menu = null;
    void Start() 
    {
        button_continue = this.transform.GetChild(0).GetComponent<Button>();
        button_continue.onClick.AddListener(Continue);
        button_menu = this.transform.GetChild(1).GetComponent<Button>();
        button_menu.onClick.AddListener(Menu);
    }
    
    private void Continue()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Game");
        Time.timeScale = 1.0f;
    }
    private void Menu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
}
