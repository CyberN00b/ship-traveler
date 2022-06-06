using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CompletedLevel : MonoBehaviour
{
    MenuController menu_controller = null;
    void Start() 
    {
        menu_controller = GameObject.Find("GameController").GetComponent<MenuController>();
        this.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(menu_controller.StartGame);
        this.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(menu_controller.ToMainMenu);
    }
}
