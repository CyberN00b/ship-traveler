using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boost_indicator : MonoBehaviour
{
    Inventory inventory = null;
    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }
    void Update()
    {
        this.GetComponent<TextMeshProUGUI>().text = "Boosts: " + inventory.GetCountOfItem("boost");
    }
}
