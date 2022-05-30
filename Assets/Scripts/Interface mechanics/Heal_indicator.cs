using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Heal_indicator : MonoBehaviour
{
    Inventory inventory = null;
    Moving ship = null;
    void Awake()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        ship = GameObject.Find("Player").GetComponent<Moving>();
    }
    void Update()
    {
        if (ship.heal_item_selected != null)
            this.GetComponent<TextMeshProUGUI>().text = (
                ship.heal_item_selected.FullNameOnUpper() + ": " + 
                inventory.GetCountOfItem(ship.heal_item_selected.item_name)
            );
        else
            this.GetComponent<TextMeshProUGUI>().text = "None";

    }
}
