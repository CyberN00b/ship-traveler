using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int _cash = 0;
    [SerializeField]
    private int _count_of_boosts = 0;
    private int iteration = 0;
    public float time_of_boost = 10f;
    public int count_of_boosts {
        get{return _count_of_boosts;}
    }
    private Moving movement = null;
    private InterfaceGenerator generator = null;
    private List<Item> items = new List<Item>();
    [SerializeField]
    private int size_of_inventory = 10;
    void Awake() 
    {
        generator = GameObject.Find("Generator").GetComponent<InterfaceGenerator>();
    }
    public bool AddItem(Item item) 
    {
            
        if (items.Count >= size_of_inventory) 
            return false;
        Item tmp = GetItem(item.item_name);
        if (tmp != null) {
            tmp.count++;
            return true;
        }
        items.Add(item);
        return true;
    }
    public int GetCountOfItem(string name) {
        Item tmp = GetItem(name);
        if (tmp == null)
            return 0;
        return tmp.count;
    }
    public bool UseItem(string name) {
        Item item = GetItem(name);
        if (item == null)
            return false;
        if (item.is_usable) {
            item.UseItem();
            generator.addEventText("You used " + name + "!").disableAfterSec(2f);
            if (item.count <= 0) {
                items.Remove(item);
            }
            return true;
        }
        return false;
    }
    private Item GetItem(string name) {
        foreach (Item item in items) {
            if (item.item_name == name) 
            {
                return item;
            }
        }
        return null;
    }
    public bool RemoveItem(string name)
    {
        Item item = GetItem(name);
        if (item == null)
            return false;
        items.Remove(item);
        return true;
    }
    public bool ChangeCash(int value)
    {
        if (-value > _cash)
            return false;
        else
            _cash += value;
        return true;
    }
}
