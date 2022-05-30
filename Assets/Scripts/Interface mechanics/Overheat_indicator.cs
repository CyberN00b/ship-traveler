using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overheat_indicator : MonoBehaviour
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
        float overheat = player.overheat;
        image.fillAmount = overheat / 100f;
        if (overheat < 50)
            image.color = Color.green;
        else
            if (50 <= overheat && overheat < 75)
            image.color = Color.yellow;
            else 
                if (75 <= overheat && overheat < 90)
                    image.color = Color.red;
                else 
                    if (90 <= overheat && !coroutine_started) 
                    {
                        coroutine_started = true;
                        StartCoroutine(CriticalOverheat());
                    }
    }
    IEnumerator CriticalOverheat()
    {
        for(; player.overheat >= 90;)
        {
            image.color = Color.yellow;
            yield return new WaitForSeconds(0.1f);
            image.color = Color.red;
            yield return new WaitForSeconds(0.1f);
        }
        image.color = Color.red;
        coroutine_started = false;
    }
}
