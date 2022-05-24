using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public bool is_usable = false;
    public string item_name = "";
    public int count = 1;
    public virtual void UseItem() 
    {
        // item functionality
    }
}
