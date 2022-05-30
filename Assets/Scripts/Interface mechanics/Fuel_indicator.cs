using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fuel_indicator : MonoBehaviour
{
    Moving player = null;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Moving>();
    }
    void Update()
    {
        this.GetComponent<TextMeshProUGUI>().text = "Fuel: " + player.fuel.ToString("0.00");
    }
}
