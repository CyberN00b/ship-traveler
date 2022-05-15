using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    [SerializeField]
    private GameObject _gameOver;

    [SerializeField]
    private GameObject _text;

    private GameObject _player;

    

    void Start()
    {
        _player = GameObject.Find("Player");
        StartCoroutine(IncreaseCanvas());
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
    
    private IEnumerator IncreaseCanvas()
    {
        Moving _controller = _player.GetComponent<Moving>();
        Animation _anim = _text.GetComponent<Animation>();
        while (Mathf.Abs(_controller.speed) > 0.1f || _controller.fuel > 0f) { yield return new WaitForSeconds(1f); }

        _anim.Play();
        yield return new WaitForSeconds(2f);
        _gameOver.SetActive(true);
        _text.SetActive(false);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
