using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbo : Bonus
{
    void Awake() {
        _spawnY = 1f;
        _frequency = 8;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") {
            Inventory inventory = other.GetComponent<Inventory>();
            Item_boost boost = new Item_boost();
            if (inventory.AddItem(boost)) {
                interface_generator.addEventText("You picked up the booster!").disableAfterSec(1.5f);
                Destroy(gameObject);
            }
        }
    }
}
