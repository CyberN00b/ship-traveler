using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_boost : Item
{
    private Moving ship = null;
    public Item_boost()
    {
        item_name = "boost";
        is_usable = true;
        ship = GameObject.Find("Player").GetComponent<Moving>();
    }
    public override void UseItem() 
    {
        count -= 1;
        ship.is_boosted = true;
        ship.boost_amount = 100;
    }
}
