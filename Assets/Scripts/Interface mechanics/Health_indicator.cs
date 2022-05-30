using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_indicator : MonoBehaviour
{
    Moving player = null;
    private Image image = null;
    private bool coroutine_started = false;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Moving>();
        image = this.GetComponent<Image>();
    }
    void Update()
    {
        image.fillAmount = (float)player.health / (float)player.max_health;
        if (image.fillAmount <= 0.3f && !coroutine_started) 
        {
            coroutine_started = true;
            StartCoroutine(CriticalHealth());
        }
    }
    IEnumerator CriticalHealth() 
    {
        for(;image.fillAmount <= 0.3f;) 
        {
            image.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            image.color = Color.red;
            yield return new WaitForSeconds(0.1f);
        }
        image.color = Color.red;
        coroutine_started = false;
    }
}
