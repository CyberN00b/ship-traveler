using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Heal_indicator : MonoBehaviour
{
    Inventory inventory = null;
    Moving ship = null;
    TextMeshProUGUI text = null;
    void Awake()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        ship = GameObject.Find("Player").GetComponent<Moving>();
        text = this.GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (ship.heal_item_selected != null) 
        {
            int count = inventory.GetCountOfItem(ship.heal_item_selected.item_name);
            if (count == 0 || !inventory.CanUseItem(ship.heal_item_selected.item_name)) 
                text.color = Color.grey;
            else
                text.color = Color.black;
            text.text = ship.heal_item_selected.FullNameOnUpper() + ": " + count;
        } else 
        {
            text.color = Color.grey;
            text.text = "None";
        }

    }
}
