using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : Bonus
{
    new void Awake()
    {
        base.Awake();
        _spawnY = 0.5f;
        _frequency = 5;
    }
    private float _count_of_gas = 10f;
    public float count_of_gas {
        get {return _count_of_gas;}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") {
            Moving fuelcontroller = other.GetComponent<Moving>();
            fuelcontroller.fuel += _count_of_gas;
            interface_generator.addEventText("+" + _count_of_gas + " fuel").disableAfterSec(1.5f);
            Destroy(gameObject);
        }
    }
}
