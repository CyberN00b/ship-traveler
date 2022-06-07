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
    private GameObject player;
    private MenuController menu_controller = null;
    private void Start()
    {
        menu_controller = GameObject.Find("GameController").GetComponent<MenuController>();
        _PauseMenu.SetActive(false);
        player = GameObject.Find("Player");
        this.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(menu_controller.StartGame);
        this.transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(menu_controller.ToMainMenu);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu_controller.IsEnabled(_PauseMenu.name))
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
        menu_controller.DisableMenu();
    }
    public void Pause()
    {
        menu_controller.EnableMenu(_PauseMenu, 1);
    }
}
