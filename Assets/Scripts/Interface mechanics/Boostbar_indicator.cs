using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boostbar_indicator : MonoBehaviour
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
        image.fillAmount = player.boost_amount / 100f;
    }
}
