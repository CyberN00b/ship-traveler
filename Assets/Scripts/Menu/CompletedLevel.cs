using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompletedLevel : MonoBehaviour
{
    public void Continue()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }
    public void Menu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
}
