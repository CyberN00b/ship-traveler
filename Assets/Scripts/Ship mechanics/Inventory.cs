using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int _cash = 0;
    [SerializeField]
    private int _count_of_boosts = 0;
    public float time_of_boost = 10f;
    public int count_of_boosts {
        get{return _count_of_boosts;}
    }
    private Moving movement = null;
    private InterfaceGenerator generator = null;
    void Awake() 
    {
        generator = GameObject.Find("Generator").GetComponent<InterfaceGenerator>();
    }
    public bool ChangeCash(int value)
    {
        if (-value > _cash)
            return false;
        else
            _cash += value;
        return true;
    }
    public void AddBoost()
    {
        _count_of_boosts += 1;
    }
    public void UseBoost()
    {
        _count_of_boosts -= 1;
        generator.addEventText("You used boost!").disableAfterSec(2f);
    }
}
