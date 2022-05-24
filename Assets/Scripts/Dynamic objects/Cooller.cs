using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooller : Bonus
{
    private void Awake()
    {
        _spawnY = 1.5f;
        _frequency = 5;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") {
            Inventory inventory = other.GetComponent<Inventory>();
            Item_wrench wrench = new Item_wrench();
            if (inventory.AddItem(wrench)) {
                interface_generator.addEventText("You picked up the cooller!").disableAfterSec(1.5f);
                Destroy(gameObject);
            }
        }
    }
}
