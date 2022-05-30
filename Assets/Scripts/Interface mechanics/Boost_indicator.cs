using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boost_indicator : MonoBehaviour
{
    Inventory inventory = null;
    TextMeshProUGUI text = null;
    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        text = this.GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        int count = inventory.GetCountOfItem("boost");
        if (count == 0)
            text.color = Color.grey;
        else
            text.color = Color.black;
        text.text = "Boosts: " + count;
    }
}
