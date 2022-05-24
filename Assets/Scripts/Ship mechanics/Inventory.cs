using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int _cash = 0;
    [SerializeField]
    private int iteration = 0;
    public float time_of_boost = 10f;
    private Moving movement = null;
    private InterfaceGenerator generator = null;
    private List<Item> items = new List<Item>();
    private int _count_of_items = 0;
    [SerializeField]
    private int size_of_inventory = 1;
    void Awake() 
    {
        generator = GameObject.Find("Generator").GetComponent<InterfaceGenerator>();
    }
    public bool AddItem(Item item) 
    {
        if (_count_of_items >= size_of_inventory) {
            generator.addEventText("You can't take one more!").disableAfterSec(2f);
            return false;
        }
        Item tmp = GetItem(item.item_name);
        _count_of_items++;
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
        int count = item.count;
        if (item.is_usable && item.UseItem()) {
            if (item.count != count) {
                _count_of_items -= count - item.count;
            }
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
    bool RemoveItem(string name)
    {
        Item item = GetItem(name);
        if (item == null)
            return false;
        _count_of_items -= item.count;
        items.Remove(item);
        return true;
    }
    public bool RemoveAllItem(string name)
    {
        Item item = GetItem(name);
        if (item == null)
            return false;
        item.count--;
        _count_of_items--;
        if (item.count <= 0)
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
