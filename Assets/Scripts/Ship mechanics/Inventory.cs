using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int _cash = 0;
    public bool ChangeCash(int value) {
        if (-value > _cash)
            return false;
        else
            _cash += value;
        return true;
    }
}
