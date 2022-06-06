using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_cooller: Item
{
    private Moving ship = null;
    private int _cool = 30;
    
    public Item_cooller()
    {
        _audio = GameObject.Find("Cooller").GetComponent<AudioSource>();
        item_name = "cooller";
        full_name = "cooller";
        is_usable = true;
        time_of_wait = 5f;
        ship = GameObject.Find("Player").GetComponent<Moving>();
    }
    public override bool UseItem() 
    {
        count--;
        ship.overheat -= _cool;
        return true;
    }
}
