using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public AudioSource _audio;
    public bool is_usable = false;
    public string item_name = "";
    public string full_name = "";
    public float time_of_wait = 0; // time of wait after use
    public int count = 1;
    public virtual bool UseItem() 
    {
        // item functionality
        return true;
    }
    public string FullNameOnUpper() 
    {
        return full_name.Substring(0, 1).ToUpper() + full_name.Remove(0, 1);
    }
}