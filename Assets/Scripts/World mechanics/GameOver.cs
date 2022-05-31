using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    [SerializeField]
    private GameObject _gameOver;

    private GameObject _player;
    private InterfaceGenerator generator = null; 
    private MenuController menu_controller = null;

    void Start()
    {
        _player = GameObject.Find("Player");
        generator = GameObject.Find("Generator").GetComponent<InterfaceGenerator>();
        menu_controller = GameObject.Find("GameController").GetComponent<MenuController>();
        _gameOver.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(menu_controller.StartGame);
        _gameOver.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(menu_controller.ToMainMenu);
        StartCoroutine(IncreaseCanvas());
    }
    private void RestartCoroutine() 
    {
        StopCoroutine(IncreaseCanvas());
        StartCoroutine(IncreaseCanvas());
    }
    public void EndOfHealth() 
    {
        StartCoroutine(endOfHealth());
    }
    private IEnumerator endOfHealth() 
    {
        generator.addEventText("Ship is destroyed!");
        yield return new WaitForSeconds(2f);
        GameIsOver();
    }
    private IEnumerator IncreaseCanvas()
    {
        Moving player = _player.GetComponent<Moving>();
        while (player.fuel > 0f)
        {
            yield return new WaitForSeconds(1f);  
        }
        EventText txt = generator.addEventText("Ran out of fuel!");
        while (Mathf.Abs(player.speed) > 0.1f && player.fuel <= 0f)
        {
            yield return new WaitForSeconds(1f);
        }
        if (player.fuel > 0f) 
        {
            txt.hideAndDisable();
            RestartCoroutine();
        }
        Inventory inventory = _player.GetComponent<Inventory>();
        OilBase oilBase = new OilBase();
        while (
            generator.isEventActive("end_level") || 
            (generator.isEventActive("oil_base") && inventory.cash >= oilBase.barrel_cost)
        ) {
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(2f);
        GameIsOver();
    }
    void GameIsOver() 
    {
        menu_controller.EnableMenu(_gameOver, 2);
    } 
}
