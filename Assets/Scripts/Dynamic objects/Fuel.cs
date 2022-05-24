using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : Bonus
{
    void Awake() {
        _spawnY = 0.5f;
        _frequency = 5;
    }
    private float count_of_gas = 10f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") {
            Moving fuelcontroller = other.GetComponent<Moving>();
            fuelcontroller.fuel += count_of_gas;
            interface_generator.addEventText("+" + count_of_gas + " fuel").disableAfterSec(1.5f);
            Destroy(gameObject);
        }
    }
}
