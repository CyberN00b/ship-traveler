using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_wrench : Item
{
    private Moving ship = null;
    private int _heal = 30;
    public Item_wrench()
    {
        _audio = GameObject.Find("Wrench").GetComponent<Wrench>().sound_item;
        item_name = "wrench";
        full_name = "wrench";
        is_usable = true;
        time_of_wait = 5f;
        ship = GameObject.Find("Player").GetComponent<Moving>();
    }
    public override bool UseItem() 
    {
        count--;
        ship.health += _heal;
        return true;
    }
}
