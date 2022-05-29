using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_boost : Item
{
    private Moving ship = null;
    private float _time_of_boost = 10f;
    public float time_of_boost {
        get {return _time_of_boost;}
    }
    public Item_boost()
    {
        _audio = GameObject.Find("Turbo").GetComponent<AudioSource>();
        item_name = "boost";
        full_name = "booster";
        is_usable = true;
        ship = GameObject.Find("Player").GetComponent<Moving>();
    }
    public override bool UseItem() 
    {
        count--;
        ship.is_boosted = true;
        ship.boost_amount = 100;
        return true;
    }
}
