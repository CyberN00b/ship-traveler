using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbo : Bonus
{
    void Awake() {
        _spawnY = 1f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") {
            Moving fuelconstroller = other.GetComponent<Moving>();
            fuelconstroller.speed *= 1.2f;
            interface_generator.addEventText("You got boost!").disableAfterSec(1.5f);
            Destroy(gameObject);
        }
    }
}
