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
            Inventory inventory = other.GetComponent<Inventory>();
            inventory.AddBoost();
            interface_generator.addEventText("You picked up the boost!").disableAfterSec(1.5f);
            Destroy(gameObject);
        }
    }
}
